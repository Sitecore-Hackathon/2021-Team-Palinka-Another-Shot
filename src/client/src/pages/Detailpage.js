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
        <h2>
          Details of the <a
          href={`/sitecore/shell/Applications/Content%20Editor.aspx?fo=${itemDetails.Id}&la=${itemDetails.Language}&sc_lang=en`}
          target="_blank" rel="noreferrer">{itemDetails.Name}</a> item <span>({itemDetails.Id})</span></h2>
        <h3>Item name: {itemDetails && itemDetails.Name}</h3>
        <h4>Icon: <img src={itemDetails && itemDetails.Icon} alt=""/></h4>
        <h4>Path: {itemDetails && itemDetails.FullPath}</h4>
        <h4>Language: {itemDetails.Language || "-"}</h4>
        <h4>Version number: {itemDetails.VersionNumber || "-"}</h4>
        {itemDetails.MultiVariateTestedRenderings && itemDetails.MultiVariateTestedRenderings.length > 0 &&
        <h5>This item has <b>Multivariate</b> renderings!</h5>
        }
        {itemDetails.PersonalizedRenderings && itemDetails.PersonalizedRenderings.length > 0 &&
        <h5>This item has <b>Personalization</b> renderings!</h5>
        }

        <div className="details-page__history">
          <h2>History of the item</h2>
          <table>
            <tbody>
            {
              itemDetails && itemDetails.History && itemDetails.History.map((history, index) => {
                return (
                  <tr key={`h-${index}-0`}>
                    <td>
                      <table>
                        <tbody>
                        <tr key={`h-${index}-1`}>
                          <th>Date:</th>
                          <td>{history.Date}</td>
                        </tr>
                        <tr key={`h-${index}-2`}>
                          <th>State change:</th>
                          <td>It was moved
                            from <b>{history.OldStateName}</b> to <b>{history.NewStateName}</b> by <b>{history.User}</b>
                          </td>
                        </tr>
                        <tr key={`h-${index}-3`}>
                          <th>Comment:</th>
                          <td>{history.CommentFields[0].Value}</td>
                        </tr>
                        </tbody>
                      </table>
                    </td>
                  </tr>
                );
              })
            }
            </tbody>
          </table>
        </div>
      </div>
    </Layout>
  );
};

export default Detailpage;