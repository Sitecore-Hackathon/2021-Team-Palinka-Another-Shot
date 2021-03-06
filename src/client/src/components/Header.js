import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { fetchWorkflows, workflowSelector } from "../redux/slices/workflows";
import { fetchWorkItems, workItemsSelector } from "../redux/slices/workitems";
import Select from "react-select";
import Logo from "../assets/logo.png";

const Header = () => {
  const dispatch = useDispatch();
  const { workflowItems, loading } = useSelector(workflowSelector);
  const { workItems } = useSelector(workItemsSelector);
  const [selectedOptionId, setSelectedOptionId] = useState(0);

  useEffect(() => {
    dispatch(fetchWorkflows());
  }, [dispatch]);

  const onChange = (option, { action }) => {
    if (action === "select-option") {
      dispatch(fetchWorkItems(option.Id));
    }
  };

  const renderQuickFilters = () => {
    if (selectedOptionId === 0 && workflowItems.length > 0) {
      const selectedOption = workflowItems.filter(option => option.IsSelected === true);
      setSelectedOptionId(selectedOption[0].Id);
      dispatch(fetchWorkItems(selectedOptionId));
    }

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
        <div className="logo">
          <img src={Logo} alt="Advanced Workbox Logo"/>
          <h1>Advanced Workbox</h1>
        </div>
      </div>
      <div className="header__bottom row middle-xs start-xs">
        {renderQuickFilters()}
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
    </header>
  );
};

export default Header;