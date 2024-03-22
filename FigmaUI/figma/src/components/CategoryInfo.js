// CategoryInfo.js
import React from 'react';

function CategoryInfo({ additionalCategory, itemCount }) {
  return (
    <div>
      Selected Category: {additionalCategory} ({itemCount})
    </div>
  );
}

export default CategoryInfo;
