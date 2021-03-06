import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Link } from "react-router-dom";
import { fetchWorkflows, workflowSelector } from "../redux/slices/workflows";
import { fetchWorkItems, workItemsSelector } from "../redux/slices/workitems";
import Select from "react-select";
import Logo from "../assets/logo.png";

const Header = (props) => {
  const quickFiltersEnabled = false;
  const dispatch = useDispatch();
  const { workflowItems, loading } = useSelector(workflowSelector);
  const { workItems } = useSelector(workItemsSelector);
  const [selectedOptionId, setSelectedOptionId] = useState(0);

  useEffect(() => {
    dispatch(fetchWorkflows());
  }, [dispatch]);

  useEffect(() => {
    if (selectedOptionId === 0 && workflowItems.length > 0) {
      const selectedOption = workflowItems.filter(option => option.IsSelected === true);
      setSelectedOptionId(selectedOption[0].Id);
      dispatch(fetchWorkItems(selectedOption[0].Id));
    }
  }, [dispatch, selectedOptionId, workflowItems]);

  const onChange = (option, { action }) => {
    if (action === "select-option") {
      dispatch(fetchWorkItems(option.Id));
    }
  };

  const renderQuickFilters = () => {
    if (!workItems.QuickFilters) {
      return (
        <div className="col col-no-gutter">&nbsp;</div>
      );
    }

    return (
      <>
        <h6>Quick filters:</h6>
        <div className="col col-no-gutter">
          <ul className="quick-filters">
            {
              Object.keys(workItems.QuickFilters || {}).map(key => {
                return (
                  <li key={key}>
                    <button>{key}</button>
                  </li>
                );
              })
            }
          </ul>
        </div>
      </>
    );
  };

  return (
    <header className="header">
      <div className="header__top row middle-xs between-xs">
        <Link
          to={`${process.env.PUBLIC_URL}`}
          className="logo"
        >
          <img src={Logo} alt="Advanced Workbox Logo"/>
          <h1>Advanced Workbox</h1>
        </Link>
        <Link
          className="launchpad"
          target="_blank"
          to={`/sitecore/shell/sitecore/client/Applications/Launchpad`}
        >Open Launchpad
        </Link>
      </div>
      {props.showFilter &&
      <div className={`header__bottom row middle-xs ${quickFiltersEnabled ? "start-xs": "end-xs"}`}>
        {quickFiltersEnabled && renderQuickFilters()}
        {workflowItems.length > 0 &&
        <Select
          className="workflow-selector"
          placeholder="Select workflow"
          isLoading={loading}
          isClearable={false}
          isSearchable={false}
          onChange={onChange}
          options={workflowItems}
          defaultValue={workflowItems.filter(option => option.IsSelected === true)}
          getOptionLabel={opt => opt.Name}
          getOptionValue={opt => opt.Id}
        />
        }
      </div>
      }
    </header>
  );
};

export default Header;