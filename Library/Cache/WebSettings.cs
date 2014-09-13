using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Linq;

namespace Library.Cache
{
    [DataContract]
    public class WebSettings
    {
        private IEnumerable<SiteConfiguration> m_sites;

        /// <summary>
        /// Gets the list of site configuration objects
        /// </summary>
        /// <returns>list of SiteConfiguration objects</returns>
        public IEnumerable<SiteConfiguration> Sites
        {
            get
            {
                if (m_sites == null)
                    LoadSettings();

                return m_sites;
            }
        }

        /// <summary>
        /// Loads the site configuration settings from the designated config file
        /// </summary>
        private void LoadSettings()
        {
            var doc = XDocument.Load(HttpContext.Current.Server.MapPath("~/Config/SiteSettings.config"));

            var config = new List<SiteConfiguration>();

            var currentEnv = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["Environment"]) ? ConfigurationManager.AppSettings["Environment"] : "development";

            // get sites
            foreach (var bundle in doc.Descendants("site"))
            {
                var environments = bundle.Descendants("environment").Where(x => x.Attribute("name") != null && x.Attribute("name").Value.Equals(currentEnv, StringComparison.InvariantCultureIgnoreCase));

                foreach (var env in environments)
                {
                    var siteConfiguration = new SiteConfiguration();

                    try
                    {
                        var envName = env.Attribute("name");
                        var rootElement = env.Descendants("root");
                        var configurationElement = env.Descendants("configuration");
                        var sitesettingsElement = env.Descendants("sitesettings");
                        var devsettingsElement = env.Descendants("devsettings");
                        var error404Element = env.Descendants("error404node");

                        siteConfiguration.Environment = envName.Value.Trim();
                        siteConfiguration.RootNodeId = int.Parse(rootElement.First().Attribute("id").Value);
                        siteConfiguration.ConfigurationNodeId = int.Parse(configurationElement.First().Attribute("id").Value);
                        siteConfiguration.SiteSettingsNodeId = int.Parse(sitesettingsElement.First().Attribute("id").Value);
                        siteConfiguration.DevSettingsNodeId = int.Parse(devsettingsElement.First().Attribute("id").Value);
                        siteConfiguration.Error404NodeId = int.Parse(error404Element.First().Attribute("id").Value);
                    }
                    catch
                    {
                        continue;
                    }

                  var securenodesElement = env.Descendants("nodesecurity");
                    
                    if (securenodesElement.Any())
                    {
                        var nodes = securenodesElement.First().Descendants("nodes");
                        var users = securenodesElement.First().Descendants("users");

                        if (nodes.Any())
                        {
                            var idList = new List<int>();

                            foreach (var node in nodes.Descendants("node"))
                            {
                                var nodeId = 0;

                                if (int.TryParse(node.Value, out nodeId))
                                    idList.Add(nodeId);
                            }

                            siteConfiguration.SecureNodeIds = idList;
                        }

                        if (users.Any())
                        {
                            var idList = new List<int>();

                            foreach (var user in users.Descendants("user"))
                            {
                                var userId = 0;

                                if (int.TryParse(user.Value, out userId))
                                    idList.Add(userId);
                            }

                            siteConfiguration.SecureAdminUserIds = idList;
                        }
                    }

                    config.Add(siteConfiguration);
                }
            }

            m_sites = config;
        }
    }

   

    [DataContract]
    public class SiteConfiguration
    {
        public int RootNodeId { get; set; }
        public int ConfigurationNodeId { get; set; }
        public int SiteSettingsNodeId { get; set; }
        public int DevSettingsNodeId { get; set; }
        public int Error404NodeId { get; set; }

        public string Environment { get; set; }
        public string AssetPath { get; set; }

        public IEnumerable<int> SecureNodeIds { get; set; }
        public IEnumerable<int> SecureAdminUserIds { get; set; }
    }
}


