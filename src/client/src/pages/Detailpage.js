import React from "react";
import Layout from "../components/Layout";
import Board from "../components/Board";
import { useParams, withRouter } from "react-router-dom";

const Detailpage = () => {
  let { id } = useParams();
  return (
    <Layout>
      <h2>hello</h2>
      <h3>{id}</h3>
    </Layout>
  );
};

export default Detailpage;