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
        <h2>{itemDetails && itemDetails.Name}  <img src={itemDetails && itemDetails.Icon}/></h2>
        <h3>{itemDetails && itemDetails.FullPath}</h3>
        
        <table>
          <tbody>
            <th>
              <td>Language</td>
              <td>{itemDetails && itemDetails.Language}</td>
            </th>
          </tbody>
        </table>

      </div>
    </Layout>
  );
};

export default Detailpage;