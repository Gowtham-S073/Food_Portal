import React, { useEffect, useState, useContext } from "react";
import styles from "./Summary.module.css";
import { ApiContext } from "./../ApiContextProvider";
import Header from "../food_Selection/Components/header";
import {Link, useNavigate } from "react-router-dom";
import designimg from "../../Assets/designimg.png";
import logLogo from "../../Assets/loglogo.png";
import errpage from "../../Assets/loginPage .png";
import { toast } from 'react-toastify';

import {
  Typography,
  Paper,
  Table,
  TableContainer,
  TableHead,
  TableBody,
  TableRow,
  TableCell,
  Button,
  Tooltip,
} from "@mui/material";
import { Print as PrintIcon, GetApp as GetAppIcon } from "@mui/icons-material";
import jsPDF from "jspdf";
import "jspdf-autotable";
import TrackStatus from "../Trackstatus/TrackStatus";

function Summary() {
  const [orderId, setOrderId] = useState({
    idInt: 0,
    idString: "",
  });
  const { trackid, setTrackid, userName, setTrackingStatus } = useContext(ApiContext);
  var navigate = useNavigate();
  setTrackingStatus(null);
  const [dateOnly, setDateOnly] = useState("");
  const [platetotal, setplateTotal] = useState(0.0);
  const [summaryDetails, setSummaryDetails] = useState([]);
  const [additional, setAdditional] = useState(0.0);
  const [addons, setAddons] = useState(0.0);
  const [total, setTotal] = useState(0.0);
  const [totalPayable, setTotalPayable] = useState(0.0);
  const [checktrackid, setChecktrackid] = useState({
    idInt: 0,
    idString: "",
  });
  const [showdiscount, setShowdiscount] = useState(0.0);
  const [showtax, setShowtax] = useState(0.0);

  const handleInputChange = (event) => {
    setTrackid(event.target.value);
    sessionStorage.setItem("trackstatusid", event.target.value);
    setChecktrackid((prevState) => ({
      ...prevState,
      idString: event.target.value,
    }));
  };

  const calculateAddtionalTotalPrice = () => {
    let sum = 0.0;
    if (summaryDetails && summaryDetails.additionalProductInfo) {
      summaryDetails.additionalProductInfo.forEach((product) => {
        sum += product.priceTotal || 0.0;
      });
    }
    console.log(sum);
    return sum;
  };

  const calculateAddonsTotalPrice = () => {
    let sum = 0.0;
    if (summaryDetails && summaryDetails.additional) {
      summaryDetails.additional.forEach((product) => {
        sum += product.priceTotal || 0.0;
      });
    }
    console.log(sum);
    return sum;
  };

  var handleTrackOrder1 = async () => {
    try {
    console.log(checktrackid.idString);
    const apiUrl = `https://localhost:7239/api/TrackStatus/check_trackid/checktrackid?username=${encodeURIComponent(
      userName
    )}`;
    const response = await fetch(apiUrl, {
      method: "POST",
      headers: {
        accept: "text/plain",
        "Content-Type": "application/json",
        "Authorization": "Bearer " + sessionStorage.getItem('token')
      },
      body: JSON.stringify({ ...checktrackid }),
    })
        if (response.status === 200) 
          {
          var myData = await response.json();

          console.log(myData);
          handleTrackOrder2();
        } 
        else {
          const errorData = await response.json();
          throw new Error(errorData.message);
        }
    }
      catch (err) {
        toast.error(err.message, {
          position: "top-left",
        });
        sessionStorage.removeItem('trackstatusid');
      }
  };

  const handleTrackOrder2 = async () => {
    try {
    orderId.idString = sessionStorage.getItem("trackstatusid");
    console.log(orderId.idString);

    const response = await fetch("https://localhost:7239/api/TrackStatus/Get_Order_Summary/TrackId", {
      method: "POST",
      headers: {
        accept: "text/plain",
        "Content-Type": "application/json",
        "Authorization": "Bearer " + sessionStorage.getItem('token')
      },
      body: JSON.stringify({ ...orderId }),
    })
        if (response.status === 200)
        {
        var myData = await response.json();
        console.log(myData);
        setSummaryDetails(myData);
      }
      else {
        const errorData = await response.json();
        throw new Error(errorData.message);
      }
    }
    catch (err) {
      toast.error(err.message, {
        position: "top-left",
      });
    }
  };

  useEffect(() => {
    if (sessionStorage.getItem('trackstatusid'))
    {
      let ignore = false;

    const handleTrackOrder = async () => {
      try {
      orderId.idString = sessionStorage.getItem("trackstatusid");

      const response = await fetch(
        "https://localhost:7239/api/TrackStatus/Get_Order_Summary/TrackId",
        {
          method: "POST",
          headers: {
            accept: "text/plain",
            "Content-Type": "application/json",
            "Authorization": "Bearer " + sessionStorage.getItem('token')
          },
          body: JSON.stringify({ ...orderId }),
        }
      )
      
          if (!ignore) {
            if (response.status === 200)
           {
            var myData = await response.json();
            console.log(orderId);
            console.log(myData);
            setSummaryDetails(myData);
           }
           else {
            const errorData = await response.json();
            throw new Error(errorData.message);
          }
        }    
      }
      catch (err) {
        toast.error(err.message, {
          position: "top-left",
        });
      }
    };

    if (!ignore) {
      handleTrackOrder();
    }

    return () => {
      ignore = true;
    };
    }
  }, [orderId]);

  useEffect(() => {
    const deliveryDate = summaryDetails?.deliveryDate?.split("T")[0] ?? "";
    setDateOnly(deliveryDate);

    const multipliedValue =
      summaryDetails?.count * summaryDetails?.plateCost ?? 0;
    setplateTotal(multipliedValue);
    const Addtionalsum = calculateAddtionalTotalPrice();
    setAdditional(Addtionalsum);
    const Addonsum = calculateAddonsTotalPrice();
    setAddons(Addonsum);
    const tempTotal = multipliedValue + Addtionalsum + Addonsum;
    const discount = tempTotal * 0.1;
    setShowdiscount(discount.toFixed(2));
    const costafterdiscount = tempTotal - discount;
    const tax = costafterdiscount * 0.1;
    setShowtax(tax.toFixed(2));
    const costwithtax = tax + costafterdiscount;
    console.log(costwithtax);
    setTotal(tempTotal);
    const formattedValue = costwithtax.toFixed(2);

    setTotalPayable(formattedValue);
  }, [summaryDetails]);

  const handlePrint = () => {
    const doc = new jsPDF();
    doc.text("Food Hut", 100, 10);
    // Add the table content using autoTable
    doc.autoTable({
      head: [["Heading", "Data"]],
      body: [
        ["Order ID", summaryDetails.orderId],
        ["Delivery Date", dateOnly],
        ["Time", summaryDetails.deliveryTime],
        ["Customer Name", summaryDetails.name],
        ["Customer Contact Number", summaryDetails.phoneNumber],
        ["Customer Email", summaryDetails.email],
        ["Delivery Address", summaryDetails.address],
        ["Allergies", summaryDetails.allergies],
        ["Plate Size", summaryDetails.plateSize],
        ["Guest Count", summaryDetails.count],
        ["Menu", summaryDetails.menu],
      ],
      startY: 20,
    });
    const menuColumnStyles = {
      0: {
        cellWidth: 60,
      },
      1: {
        cellWidth: 40,
      },
    };
    const billY = doc.autoTable.previous.finalY + 8;
    doc.text("Billing Details", 90, billY);

    doc.autoTable({
      head: [["Menu Items", "Quantity", "Plate/Price", "Total Price"]],
      body: [
        [
          summaryDetails.menu,
          summaryDetails.count,
          summaryDetails.plateCost,
          `$${platetotal}`,
        ],
      ],
      columnStyles: menuColumnStyles,
      startY: doc.autoTable.previous.finalY + 10,
    });

    if (summaryDetails.additional && summaryDetails.additional.length > 0) {
      const secondTableData = [
        ["Addon Name", "Quantity", "Price", "Total Price"],
        ...summaryDetails.additional.map((addon) => [
          addon.addonName,
          addon.quantity,
          `$${addon.price}`,
          `$${addon.priceTotal}`,
        ]),
      ];

      const secondTableColumnStyles = {
        2: {
          cellWidth: 40,
        },
        3: {
          cellWidth: 40,
        },
      };

      doc.autoTable({
        body: secondTableData,
        startY: doc.autoTable.previous.finalY + 10,
        columnStyles: secondTableColumnStyles,
      });
    }

    if (
      summaryDetails.additionalProductInfo &&
      summaryDetails.additionalProductInfo.length > 0
    ) {
      const thirdTableData = [
        ["Product Name", "Quantity", "Price", "Total Price"],
        ...summaryDetails.additionalProductInfo.map((additionalproduct) => [
          additionalproduct.additionalProductName,
          additionalproduct.quantity,
          `$${additionalproduct.price}`,
          `$${additionalproduct.priceTotal}`,
        ]),
      ];
      const thirdTableColumnStyles = {
        1: {
          cellWidth: 40,
        },
        2: {
          cellWidth: 40,
        },
        3: {
          cellWidth: 40,
        },
      };

      doc.autoTable({
        body: thirdTableData,
        startY: doc.autoTable.previous.finalY + 10,
        columnStyles: thirdTableColumnStyles,

      });
    }

    
    const lastY = doc.autoTable.previous.finalY + 10;
    doc.text(`Total Amount: $${totalPayable}`, 112, lastY);

    doc.save("invoice.pdf");
  };
  const gotohome = () => {
    navigate("/home");
  };

  if (!sessionStorage.getItem('trackstatusid'))
  {
    return(
      <div>

        <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'flex-start', justifyContent: 'center', height: '70vh', paddingLeft: '20px' }}>
          <img className="loglogo" src={logLogo} alt="" /> <p style={{

            padding: "20px",
            fontSize: "38px",
            color: "black",
            textAlign: "center",
          }}>
            Enter the TrackId First
          </p>
          <div className="img" style={{ position: 'fixed', bottom: 0, left: 0 }}>
            <img src={designimg} alt="" />
          </div>
          <div className="imgside" style={{ position: 'fixed', bottom: 0, right: 0 }}>
            <img src={errpage} alt="" />
          </div>
          <Link to='/trackstatus'>
            <Button sx={{
              backgroundColor: "#ff6d00", color: 'rgb(255, 255, 255)', alignItems: 'flex-end', justifyContent: "center", marginTop: '20px', marginLeft: '220px', width: '180px', height: '50px', fontWeight: 'bold', fontSize: '18px', '&:hover': {
                backgroundColor: "orangered", // Change this to your desired hover color

              }
            }} >
              Go Back
            </Button>


          </Link>
        </div>
      </div>
    )
  }
  return (
    <>
      <Header />

      <section className={styles.mobile}>
        <div className={styles.whole_section_outer_div}>
          <div className={styles.whole_section_inner_div}>
            <input
              type="submit"
              className={styles.homebutton}
              value="< Home"
              onClick={gotohome}
            />

            <div className={styles.track_btn}>
              <div className={styles.track_btn_item1}>
                <input
                  type="text"
                  className={styles.track_input}
                  placeholder={trackid}
                  onChange={handleInputChange}
                />
              </div>
              <div className={styles.track_btn_item2}>
                <input
                  type="submit"
                  className={styles.track}
                  value="Track"
                  onClick={handleTrackOrder1}
                />
              </div>
            </div>
            <div className={styles.status}>
              <div className={styles.status_item1}>
                <h1>
                  Status:<span className={styles.sta}>Pending</span>
                </h1>
              </div>
              <div className={styles.message}>
                <input
                  type="submit"
                  className={styles.message_btn}
                  value="Download"
                  onClick={handlePrint}
                />
              </div>
            </div>
            <div className={styles.whole_summary_outer_div}>
              <div className={styles.summary}>
                <div className={styles.order_summary}>
                  <div className={styles.order_summary1}>
                    <div className={styles.order_item1}>
                      <div className={styles.para}>
                        Order ID:{" "}
                        <span className={styles.order_summary_content}>
                          {summaryDetails.orderId}
                        </span>
                      </div>
                    </div>
                    <div className={styles.order_item2}>
                      <div className={styles.para}>
                        Delivery Date:{" "}
                        <span className={styles.order_summary_content}>
                          {dateOnly}
                        </span>
                      </div>
                    </div>
                  </div>
                  <div className={styles.order_summary2}>
                    <div className={styles.order_item3}>
                      <div className={styles.para}>
                        Time:{" "}
                        <span className={styles.order_summary_content}>
                          {summaryDetails.deliveryTime}
                        </span>
                      </div>
                    </div>
                  </div>
                </div>
                <div className={styles.underline} id={styles.order_underline}>
                  <hr />
                </div>
                <div className={styles.sub_head}>
                  <div className={styles.para}>Contact Details</div>
                </div>
                <div className={styles.contact_summary_containers}>
                  <div className={styles.contact_summary1}>
                    <div className={styles.contact_item1}>
                      <div className={styles.item_head}>User Name</div>
                      <div
                        className={`${styles.item_content} ${styles.name_width}`}
                      >
                        {summaryDetails.name}
                      </div>
                    </div>
                    <div className={styles.contact_item2}>
                      <div className={styles.item_head}>
                        User Contact Number
                      </div>
                      <div className={styles.item_content}>
                        +91 {summaryDetails.phoneNumber}
                      </div>
                    </div>
                    <div className={styles.contact_item3}>
                      <div className={styles.item_head}>User Email</div>
                      <div
                        className={`${styles.item_content} ${styles.email_width}`}
                      >
                        {summaryDetails.email}
                      </div>
                    </div>
                  </div>
                  <div className={styles.contact_summary2}>
                    <div className={styles.contact_item4}>
                      <div className={styles.item_head}>Delivery Address</div>
                      <div
                        className={`${styles.item_content} ${styles.address_width}`}
                      >
                        {summaryDetails.address}
                      </div>
                    </div>
                  </div>
                </div>
                <div className={styles.underline}>
                  <hr />
                </div>
                <div className={styles.sub_head}>
                  <div className={styles.para}>Other Details</div>
                </div>
                <div className={styles.other_details_summary}>
                  <div className={styles.other_details_summary1}>
                    <div className={styles.other_details_item1}>
                      <div className={styles.item_head}>Allergies</div>
                      <div
                        className={`${styles.item_content} ${styles.allergies_width}`}
                      >
                        {summaryDetails.allergies ? (
                          summaryDetails.allergies
                        ) : (
                          <p>No allergies</p>
                        )}
                      </div>
                    </div>
                    <div className={styles.other_details_item2}>
                      <div className={styles.item_head}>Plate Size</div>
                      <div
                        className={`${styles.item_content} ${styles.foodtype_width}`}
                      >
                        {summaryDetails.plateSize} ({summaryDetails.foodtype})
                      </div>
                    </div>
                  </div>
                  <div className={styles.other_details_summary2}>
                    <div className={styles.other_details_item3}>
                      <div className={styles.item_head}>Delivery Type</div>
                      <div
                        className={`${styles.item_content} ${styles.delivery_width}`}
                      >
                        {summaryDetails.deliveryType}
                      </div>
                    </div>
                    <div className={styles.other_details_item4}>
                      <div className={styles.item_head}>Group Size</div>
                      <div className={styles.item_content}>
                        {summaryDetails.groupSize} ({summaryDetails.count})
                      </div>
                    </div>
                  </div>
                </div>
                <div className={styles.underline}>
                  <hr />
                </div>
                <div className={styles.sub_head}>
                  <div className={styles.para}>Additional Details</div>
                </div>
                <div className={styles.additional_details_item}>
                  {summaryDetails.additionalNote && (
                    <div
                      className={styles.item_content}
                      id={styles.additional_con}
                    >
                      {summaryDetails.additionalNote}
                    </div>
                  )}
                  {!summaryDetails.additionalNote && (
                    <div
                      className={styles.item_content}
                      id={styles.additional_con}
                    >
                      No additionalNote
                    </div>
                  )}
                </div>
              </div>
            </div>

            <div className={styles.whole_table}>
              <div className={styles.table1}>
                <h2 className={styles.table_heading}>order details</h2>
                <div className={`${styles.row} mt-5`}>
                  <div className={styles.col}>
                    <div className={styles.table_responsive}>
                      <table
                        className={`${styles.table} ${styles.table_borderless} ${styles.custom_table1}`}
                      >
                        <thead
                          className={`${styles.thead_light} ${styles.table_header}`}
                        >
                          <tr>
                            <th
                              scope="col"
                              className={`${styles.common_thead} ${styles.table1_th1}`}
                            >
                              items
                            </th>
                            <th
                              scope="col"
                              className={`${styles.text_right} ${styles.common_thead} ${styles.table1_th2}`}
                            >
                              Qty
                            </th>
                            <th
                              scope="col"
                              className={`${styles.text_right} ${styles.common_thead} ${styles.table1_th2}`}
                            >
                              Cost/Plate
                            </th>
                            <th
                              scope="col"
                              className={`${styles.text_right} ${styles.common_thead} ${styles.table1_th2}`}
                            >
                              Amount
                            </th>
                          </tr>
                        </thead>
                        <tbody className={styles.table_body}>
                          <tr>
                            <td
                              className={`${styles.table_color_black} ${styles.tdata_common}`}
                            >
                              {summaryDetails.groupSize} :{" "}
                              {summaryDetails.foodtype}
                            </td>
                            <td
                              className={`${styles.text_right} ${styles.tdata_common} ${styles.table_color_black}`}
                            >
                              {summaryDetails.count}
                            </td>
                            <td
                              className={`${styles.text_right} ${styles.tdata_common} ${styles.table_color_black}`}
                            >
                              ${summaryDetails.plateCost}
                            </td>
                            <td
                              className={`${styles.text_right} ${styles.tdata_common} ${styles.table_color_black}`}
                            >
                              ${platetotal}
                            </td>
                          </tr>
                          <tr>
                            <td className={styles.menu}>
                              {summaryDetails.menu}
                            </td>
                          </tr>
                          <tr>
                            <td
                              colSpan="4"
                              style={{ borderTop: "1px solid grey" }}
                            ></td>
                          </tr>

                          {summaryDetails.additional &&
                            summaryDetails.additional.length > 0 && (
                              <tr>
                                <td
                                  className={`${styles.table_color_black} ${styles.tdata_common}`}
                                >
                                  Add ons
                                </td>
                              </tr>
                            )}
                          {summaryDetails.additional &&
                            summaryDetails.additional.map((addon, index) => (
                              <tr key={index}>
                                <td
                                  className={styles.tdata_common}
                                  style={{ color: "#484848" }}
                                >
                                  {addon.addonName}
                                </td>
                                <td
                                  className={`${styles.text_right} ${styles.tdata_common} ${styles.table_color_black}`}
                                >
                                  {addon.quantity}
                                </td>
                                <td
                                  className={`${styles.text_right} ${styles.tdata_common} ${styles.table_color_black}`}
                                >
                                  ${addon.price}
                                </td>
                                <td
                                  className={`${styles.text_right} ${styles.tdata_common} ${styles.table_color_black}`}
                                >
                                  ${addon.priceTotal}
                                </td>
                              </tr>
                            ))}

                          <tr></tr>

                          {summaryDetails.additional &&
                            summaryDetails.additional.length > 0 && (
                              <tr>
                                <td
                                  colSpan="4"
                                  style={{ borderTop: "1px solid grey" }}
                                ></td>
                              </tr>
                            )}

                          {summaryDetails.additionalProductInfo &&
                            summaryDetails.additionalProductInfo.length > 0 && (
                              <tr>
                                <td
                                  className={`${styles.table_color_black} ${styles.tdata_common}`}
                                >
                                  Additional Products
                                </td>
                              </tr>
                            )}

                          {summaryDetails.additionalProductInfo &&
                            summaryDetails.additionalProductInfo.map(
                              (additionalproduct, index) => (
                                <tr key={index}>
                                  <td
                                    className={styles.tdata_common}
                                    style={{ color: "#484848" }}
                                  >
                                    {additionalproduct.additionalProductName}
                                  </td>
                                  <td
                                    className={`${styles.text_right} ${styles.tdata_common} ${styles.table_color_black}`}
                                  >
                                    {additionalproduct.quantity}
                                  </td>
                                  <td
                                    className={`${styles.text_right} ${styles.tdata_common} ${styles.table_color_black}`}
                                  >
                                    ${additionalproduct.price}
                                  </td>
                                  <td
                                    className={`${styles.text_right} ${styles.tdata_common} ${styles.table_color_black}`}
                                  >
                                    ${additionalproduct.priceTotal}
                                  </td>
                                </tr>
                              )
                            )}
                          <tr></tr>

                          {summaryDetails.additionalProductInfo &&
                            summaryDetails.additionalProductInfo.length > 0 && (
                              <tr>
                                <td
                                  colSpan="4"
                                  style={{ borderTop: "1px solid grey" }}
                                ></td>
                              </tr>
                            )}

                          <tr>
                            <td className={styles.total_amount}>Total</td>
                            <td className={styles.text_right}></td>
                            <td className={styles.text_right}></td>
                            <td className={styles.text_right}>
                              <span
                                className={styles.total_amount}
                                style={{ color: "#00B373" }}
                              >
                                ${total}
                              </span>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>

              <div className={styles.table2}>
                <h2 className={styles.table_heading}>Billing details</h2>
                <div className={`${styles.row} mt-5`}>
                  <div className={styles.col}>
                    <div className={styles.table_responsive}>
                      <table
                        className={`${styles.table} ${styles.table_borderless} ${styles.custom_table2}`}
                      >
                        <thead
                          className={`${styles.thead_light} ${styles.table_header}`}
                        >
                          <tr>
                            <th
                              scope="col"
                              className={styles.common_thead}
                              style={{ width: "400px" }}
                            >
                              Payable Summary
                            </th>
                            <th
                              scope="col"
                              className={styles.text_right}
                              style={{
                                minWidth: "150px",
                                marginRight: "150px",
                              }}
                            ></th>
                          </tr>
                        </thead>
                        <tbody className={styles.table_body}>
                          <tr className={styles.Billing}>
                            <td className={styles.tdata_common}>Subtotal</td>

                            <td
                              className={`${styles.text_right} ${styles.tdata_common} ${styles.table_color_black}`}
                            >
                              ${total}
                            </td>
                          </tr>
                          <tr className={styles.Billing}>
                            <td className={styles.tdata_common}>Delivery</td>

                            <td
                              className={`${styles.text_right} ${styles.tdata_common} ${styles.table_color_black}`}
                            >
                              Free
                            </td>
                          </tr>
                          <tr className={styles.Billing}>
                            <td className={styles.tdata_common}>Discount</td>

                            <td
                              className={`${styles.text_right} ${styles.tdata_common} ${styles.table_color_black}`}
                            >
                              -${showdiscount}(10%)
                            </td>
                          </tr>
                          <tr className={styles.Billing}>
                            <td className={styles.tdata_common}>Tax</td>

                            <td
                              className={`${styles.text_right} ${styles.tdata_common} ${styles.table_color_black}`}
                            >
                              +${showtax}(10%)
                            </td>
                          </tr>

                          <tr>
                            <td
                              colSpan="2"
                              style={{
                                borderTop: "1px solid grey",
                                padding: "0px",
                              }}
                            ></td>
                          </tr>

                          <tr>
                            <td className={styles.total_amount}>
                              <span style={{ padding: "16px" }}>Total</span>
                            </td>
                            <td className={styles.text_right}>
                              <span
                                className={styles.total_amount}
                                style={{ color: "#00B373", padding: "16px" }}
                              >
                                ${totalPayable}
                              </span>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div className={styles.footer}>
          <div className={styles.footer_content}>
            <p>
              If you have any questions, please contact{" "}
              <span className={styles.colored_content}>
                support@foodhut.com
              </span>
            </p>
          </div>
        </div>
      </section>
    </>
  );
}

export default Summary;
