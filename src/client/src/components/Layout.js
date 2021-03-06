import React from "react";
import Header from "./Header";
import Footer from "./Footer";

const Layout = ({ children }) => {
  return (
    <div className="page">
      <Header/>
      <section className="content">
        {children}
      </section>
      <Footer/>
    </div>
  );
};

export default Layout;