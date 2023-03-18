using RouteComposer.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using RouteComposer.Interfaces;

namespace RouteComposer.Services
{
    public class RouteComposer : IRouteComposer
    {
        readonly Hashtable directionsSearch = new Hashtable();
        private string start;

        public void AddDirection(Direction newDirection)
        {
            //create route from direction
            var l = new LinkedList<string>(new List<string>() { newDirection.From, newDirection.To });

            //query existing routes
            var dFrom = (LinkedList<string>)directionsSearch[newDirection.From];
            var dTo = (LinkedList<string>)directionsSearch[newDirection.To];
            

            //Append/prepend with existing routes
            if (dFrom != null)
            {
                ClearSearch(dFrom);
                dFrom.Remove(dFrom.Last);
                l.PrependRange(dFrom);

            }

            if (dTo != null)
            {
                ClearSearch(dTo);
                dTo.Remove(dTo.First);
                l.AppendRange(dTo);
            }

            //hash route for later
            directionsSearch.Add(l.First.Value, l);
            directionsSearch.Add(l.Last.Value, l);

            start = l.First.Value;

        }

        public LinkedList<string> GetRoute()
        {

            if (directionsSearch.Count == 2)
            {
                return (LinkedList<string>)directionsSearch[start];
            }

            throw new Exception(Properties.Resources.InvalideRoute);

        }

        private void ClearSearch(LinkedList<string> list)
        {
            var node = list.First;
            while (node != null)
            {
                directionsSearch.Remove(node.Value);
                node = node.Next;
            }
        }
    }
}