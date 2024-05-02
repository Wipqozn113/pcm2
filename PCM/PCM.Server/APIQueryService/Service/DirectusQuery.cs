namespace PCM.Server.APIQueryService.Service
{
    public class DirectusQuery
    {
        private string _query { get; set; } = "";

        private string? _baseUrl { get; set; }

        public DirectusQuery() { }

        public DirectusQuery(string baseUrl) 
        {
            _baseUrl = baseUrl;
        }

        /***********/
        /* Getters */
        /***********/
        public string FullUrl
        {
            get
            {
                if (_baseUrl is null)
                    throw new ArgumentNullException(nameof(_baseUrl));

                return _baseUrl + _query;
            }
        }

        public string QueryString => _query;

        /******************/
        /* Query Updaters */
        /******************/

        public DirectusQuery IsEqual(string name, string value)
        {
            UpdateQuery(name, value, "eq");

            return this;
        }

        public DirectusQuery IsEqual(string name, long value)
        {
            UpdateQuery(name, value, "eq");

            return this;
        }

        public DirectusQuery IsNotEqual(string name, string value)
        {
            UpdateQuery(name, value, "neq");

            return this;
        }

        public DirectusQuery IsNotEqual(string name, long value)
        {
            UpdateQuery(name, value, "neq");

            return this;
        }


        public DirectusQuery LessThan(string name, long value)
        {
            UpdateQuery(name, value, "lt");

            return this;
        }

        public DirectusQuery LessThan(string name, string value)
        {
            UpdateQuery(name, value, "lt");

            return this;
        }

        public DirectusQuery LessThanOrEqual(string name, string value)
        {
            UpdateQuery(name, value, "lte");

            return this;
        }

        public DirectusQuery LessThanOrEquall(string name, long value)
        {
            UpdateQuery(name, value, "lte");

            return this;
        }

        public DirectusQuery GreaterThan(string name, long value)
        {
            UpdateQuery(name, value, "gt");

            return this;
        }

        public DirectusQuery GreaterThan(string name, string value)
        {
            UpdateQuery(name, value, "gt");

            return this;
        }

        public DirectusQuery GreaterThanOrEqual(string name, string value)
        {
            UpdateQuery(name, value, "gte");

            return this;
        }

        public DirectusQuery GreaterThanOrEquall(string name, long value)
        {
            UpdateQuery(name, value, "gte");

            return this;
        }

        public DirectusQuery In<T>(string name, IEnumerable<T> values) where T : class
        {
            string value = "";
            foreach(var val in values)
            {
                if (!string.IsNullOrEmpty(value))
                    value += ",";

                value += val.ToString();
            }

            UpdateQuery(name, value, "in");

            return this;
        }

        public DirectusQuery NotIn<T>(string name, IEnumerable<T> values) where T : class
        {
            string value = "";
            foreach (var val in values)
            {
                if (!string.IsNullOrEmpty(value))
                    value += ",";

                value += val.ToString();
            }

            UpdateQuery(name, value, "nin");

            return this;
        }

        public DirectusQuery IsNull(string name)
        {
            UpdateQuery($"[{name}][null]");

            return this;
        }

        public DirectusQuery IsNotNull(string name)
        {
            UpdateQuery($"[{name}][nnull]");

            return this;
        }

        public DirectusQuery Contains(string name, string value, bool caseSensitive = true)
        {
            if(caseSensitive)
            {
                UpdateQuery(name, value, "contains");
            }
            else
            {
                UpdateQuery(name, value, "icontains");
            }

            return this;
        }

        public DirectusQuery DoesNotContains(string name, string value)
        {
            UpdateQuery(name, value, "ncontains");

            return this;
        }

        public DirectusQuery StartsWith(string name, string value, bool caseSensitive = true)
        {
            if (caseSensitive)
            {
                UpdateQuery(name, value, "starts_with");
            }
            else
            {
                UpdateQuery(name, value, "istarts_with");
            }

            return this;
        }

        public DirectusQuery DoesNotStartWith(string name, string value, bool caseSensitive = true)
        {
            if (caseSensitive)
            {
                UpdateQuery(name, value, "nstarts_with");
            }
            else
            {
                UpdateQuery(name, value, "instarts_with");
            }

            return this;
        }

        public DirectusQuery EndsWith(string name, string value, bool caseSensitive = true)
        {
            if (caseSensitive)
            {
                UpdateQuery(name, value, "ends_with");
            }
            else
            {
                UpdateQuery(name, value, "iends_with");
            }

            return this;
        }

        public DirectusQuery DoesNotEndWith(string name, string value, bool caseSensitive = true)
        {
            if (caseSensitive)
            {
                UpdateQuery(name, value, "nends_with");
            }
            else
            {
                UpdateQuery(name, value, "inends_with");
            }

            return this;
        }

        public DirectusQuery Between(string name, long lowerValue, long higherValue)
        {
            // Lower Value MUST come first, so let's swap those values if needed
            if (higherValue < lowerValue)
            {
                var temp = lowerValue;
                lowerValue = higherValue;
                higherValue = temp;
            }

            var value = $"{lowerValue},{higherValue}";
            UpdateQuery(name, value, "between");

            return this;                
        }

        public DirectusQuery NotBetween(string name, long lowerValue, long higherValue)
        {
            // Lower Value MUST come first, so let's swap those values if needed
            if (higherValue < lowerValue)
            {
                var temp = lowerValue;
                lowerValue = higherValue;
                higherValue = temp;
            }

            var value = $"{lowerValue},{higherValue}";
            UpdateQuery(name, value, "nbetween");

            return this;
        }

        public DirectusQuery IsEmpty(string name)
        {
            UpdateQuery($"[{name}][empty]");

            return this;
        }

        public DirectusQuery IsNotEmpty(string name)
        {
            UpdateQuery($"[{name}][nempty]");

            return this;
        }

        /*******************/
        /* Private Methods */
        /*******************/
        private void UpdateQuery(string query)
        {
            _query += string.IsNullOrEmpty(_query) ? "?" : "&";
            _query += query;
        }

        private void UpdateQuery(string name, string value, string condition)
        {
            _query += string.IsNullOrEmpty(_query) ? "?" : "&";
            _query += $"[{name}][{condition}]={value}";
        }

        private void UpdateQuery(string name, long value, string condition)
        {
            _query += string.IsNullOrEmpty(_query) ? "?" : "&";
            _query += $"[{name}][_{condition}]={value}";
        }

    }
}
