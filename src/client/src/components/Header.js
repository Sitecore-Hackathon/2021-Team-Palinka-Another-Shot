import React from "react";
import Select from "react-select";
import Logo from "../assets/logo.png";

const options = [
  { value: "workflow1", label: "Standard" },
  { value: "workflow2", label: "Editorial" },
  { value: "workflow3", label: "External" }
];

const Header = () => {
  return (
    <header className="header">
      <div className="header__top row middle-xs between-xs">
        <div className="logo">
          <img src={Logo} alt="Advanced Workbox Logo"/>
          <h1>Advanced Workbox</h1>
        </div>
        <Select
          className="workflow-selector"
          placeholder="Select workflow"
          isClearable={false}
          isSearchable={false}
          options={options}
        />
      </div>
      <div className="header__bottom row middle-xs start-xs">
        <h6>Quick filters:</h6>
        <div className="col col-no-gutter">
          <ul className="quick-filters">
            <li>
              <button>Recently updated</button>
            </li>
            <li>
              <button>Another filter</button>
            </li>
          </ul>
        </div>
      </div>
    </header>
  );
};

export default Header;