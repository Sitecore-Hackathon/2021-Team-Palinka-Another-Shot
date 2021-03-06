import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { fetchWorkItems, workItemsSelector } from "../redux/slices/workitems";
import Layout from "../components/Layout";

const Sample = () => {
  const dispatch = useDispatch();
  const { workItems, loading, hasErrors } = useSelector(workItemsSelector);

  useEffect(() => {
    dispatch(fetchWorkItems());
  }, [dispatch]);

  const renderItems = () => {
    if (loading) {
      return <p>Loading items...</p>;
    }
    if (hasErrors) {
      return <p>Cannot display items...</p>;
    }

    return workItems.map(workItem =>
      <div key={workItem.idMeal} className='tile'>
        <h2>{workItem.strMeal}</h2>
        <img src={workItem.strMealThumb} alt=''/>
      </div>
    );
  };

  return (
    <Layout>
      <section>
        <h1>Sitecore Hackathon 2021</h1>
        {renderItems()}
      </section>
    </Layout>
  );
};

export default Sample;