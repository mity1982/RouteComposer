using System;
using System.Text.RegularExpressions;
using RouteComposer.DTO;

namespace RouteComposer.Controllers
{
    public class RoutesControllerConsole
    {
        
        private int _linesToRead = 0;
        private int _linesRead = 0;

        private Func<Interfaces.IRouteComposer> modelFactory;
        private Interfaces.IRouteComposer _routesModel;
        private readonly Regex _patternRegex = new Regex(@"^[a-zA-Z0-9_ ]+$");

        public RoutesControllerConsole()
        {
            modelFactory = () => new Services.RouteComposer();
        }

        public RoutesControllerConsole(Func<Interfaces.IRouteComposer> modelFactory)
        {
            this.modelFactory = modelFactory;
        }

        public string OnNewInput(string input)
        {
            if (_linesToRead == 0) return OnCountEnter(input);
            return OnDirectionEnter(input);
        }

        private string OnCountEnter(string input)
        {
            if (int.TryParse(input, out _linesToRead) && _linesToRead > 0)
            {
                _routesModel = modelFactory();
                return string.Empty;
            }

            return Properties.Resources.InvalidInput;
        }

        private string OnDirectionEnter(string input)
        {
            //validate
            if (!_patternRegex.IsMatch(input))
                return Properties.Resources.InvalidInput; 

            var cities = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            if(cities.Length != 2)
                return Properties.Resources.InvalidInput;

            if (cities[0] == cities[1])
                return Properties.Resources.InvalidInput;

            //update state
            _linesRead++;

            //execute command
            string result = string.Empty;
            try
            {
                _routesModel.AddDirection(new Direction(){From = cities[0], To = cities[1]});

                if (_linesToRead == _linesRead)
                {
                    result = String.Format(Properties.Resources.ResultLine, string.Join(" ", _routesModel.GetRoute()));
                    _linesToRead = 0;
                    _linesRead = 0;
                }
            }
            catch (Exception e)
            {
                result = e.Message;
            }

            return result;
        }

        public string GetPrompt()
        {
            return _linesToRead == 0 ? Properties.Resources.LineCountPrompt : string.Format(Properties.Resources.DirectionPrompt, _linesRead+1, _linesToRead);
        }

    }
}