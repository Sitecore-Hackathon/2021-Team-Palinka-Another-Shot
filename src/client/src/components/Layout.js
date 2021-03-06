import React from "react";
import Header from "./Header";
import Footer from "./Footer";

const Layout = ({ showFilter, children }) => {
  return (
    <div className="page">
      <Header showFilter={showFilter}/>
      <section className="content">
        {children}
      </section>
      <Footer/>
    </div>
  );
};

export default Layout;