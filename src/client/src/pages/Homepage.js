import React from "react";
import Layout from "../components/Layout";
import Board from "../components/Board";

const Homepage = () => {
  return (
    <Layout showFilter={true}>
      <Board />
    </Layout>
  );
};

export default Homepage;