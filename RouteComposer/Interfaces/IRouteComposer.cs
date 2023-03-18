using System.Collections.Generic;
using RouteComposer.DTO;

namespace RouteComposer.Interfaces
{
    public interface IRouteComposer
    {
        void AddDirection(Direction newDirection);
        LinkedList<string> GetRoute();
    }
}