import React from "react";
import Layout from "../components/Layout";
import Board from "../components/Board";
import { useParams, withRouter } from "react-router-dom";
import { fetchItemDetails, itemDetailsSelector } from "../redux/slices/itemDetails";
import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";

const Detailpage = () => {
  const dispatch = useDispatch();
  let { id, lang } = useParams();

  const [data, setData] = useState(null);
  

  useEffect(() => {
    dispatch(fetchItemDetails("asd","asd"));
  }, [dispatch]);


  // useEffect(() => {
  //   dispatch(fetchItemDetails(id,'en'));
  // }, [dispatch, itemDetails]);
  return (
    <Layout>
      <h2>hello</h2>
      <h3>{id}</h3>
      <h4>{lang}</h4>
    </Layout>
  );
};

export default Detailpage;