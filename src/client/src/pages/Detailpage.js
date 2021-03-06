import React, { useEffect } from "react";
import Layout from "../components/Layout";
import { useParams } from "react-router-dom";
import { fetchItemDetails, itemDetailsSelector } from "../redux/slices/itemDetails";
import { useDispatch, useSelector } from "react-redux";

const Detailpage = () => {
  const dispatch = useDispatch();
  let { id, lang } = useParams();

  const { itemDetails } = useSelector(itemDetailsSelector);

  useEffect(() => {
    dispatch(fetchItemDetails(id, lang));
  }, [dispatch, id, lang]);

  return (
    <Layout>
      <div className="details-page">
        <h2>{itemDetails && itemDetails.FullPath}</h2>
      </div>
    </Layout>
  );
};

export default Detailpage;