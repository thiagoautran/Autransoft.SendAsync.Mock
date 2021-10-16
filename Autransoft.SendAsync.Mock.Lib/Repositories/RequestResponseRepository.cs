using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Autransoft.SendAsync.Mock.Lib.Entities;

namespace Autransoft.SendAsync.Mock.Lib.Repositories
{
    internal class RequestResponseRepository
    {
        private Dictionary<RequestEntity, ResponseEntity> _listConfigurationRequestsAndResponses;

        private Dictionary<RequestEntity, ResponseEntity> ListConfigurationRequestsAndResponses
        {
            get
            {
                if(_listConfigurationRequestsAndResponses == null)
                    _listConfigurationRequestsAndResponses = new Dictionary<RequestEntity, ResponseEntity>();

                return _listConfigurationRequestsAndResponses;
            }
        }

        internal void Clean() => _listConfigurationRequestsAndResponses = null;

        internal void Add(RequestEntity requestEntity, ResponseEntity responseEntity)
        {
            var keyValuePair = ListConfigurationRequestsAndResponses.Where
            (
                x => 
                x.Key.HttpMethod == requestEntity.HttpMethod &&
                x.Key.AbsolutePath == requestEntity.AbsolutePath &&
                x.Key.Query == requestEntity.Query
            )
            .FirstOrDefault();

            if(keyValuePair.Key != null)
                ListConfigurationRequestsAndResponses.Remove(keyValuePair.Key);

            ListConfigurationRequestsAndResponses.Add(requestEntity, responseEntity);
        }

        internal KeyValuePair<RequestEntity, ResponseEntity> Get(HttpMethod HttpMethod, string absolutePath, string query) =>
            ListConfigurationRequestsAndResponses.Where
            (
                x => 
                x.Key.HttpMethod == HttpMethod && 
                x.Key.AbsolutePath == absolutePath && 
                x.Key.Query == query
            )
            .FirstOrDefault();
    }
}