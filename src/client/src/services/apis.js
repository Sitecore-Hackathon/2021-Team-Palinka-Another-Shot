const useLocalSitecore = true;

export default function getWorkflowApi() {
  if (window.location.hostname === "localhost") {
    if (useLocalSitecore) {
      return "https://cm.team-palinka.localhost/sitecore/api/ssc/workbox/workflows";
    }
    return `${process.env.PUBLIC_URL}/data/mock-workflows.json`;
  }
  return "/sitecore/api/ssc/workbox/workflows";
}

export function getWorkflowDetails() {
  if (window.location.hostname === "localhost") {
    if (useLocalSitecore) {
      return "https://cm.team-palinka.localhost/sitecore/api/ssc/workbox/detail/{workflowid}";
    }
    return `${process.env.PUBLIC_URL}/data/mock-workflow-details.json`;
  }
  return "/sitecore/api/ssc/workbox/detail/{workflowid}";
}

export function getItemDetails() {
  if (window.location.hostname === "localhost") {
    if (useLocalSitecore) {
      return "https://cm.team-palinka.localhost/sitecore/api/ssc/workbox/itemdetail?itemId={itemId}&language={language}";
    }
    return `${process.env.PUBLIC_URL}/data/mock-item-details.json`;
  }
  return "/sitecore/api/ssc/workbox/itemdetail?itemId={itemId}&language={language}";
}

export function getChangeWorkflowApi() {
  if (window.location.hostname === "localhost") {
    return "https://cm.team-palinka.localhost/sitecore/api/ssc/workbox/ChangeWorkflow";
  }
  return "/sitecore/api/ssc/workbox/ChangeWorkflow";
}

