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

        /****************/
        /* Field Methos */
        /****************/

        public DirectusQuery SetFields(IEnumerable<string> fields)
        {
            UpdateFields(string.Join(",", fields));

            return this;
        }

        public DirectusQuery SetFields(string fields)
        {
            UpdateFields(fields);

            return this;
        }

        /******************/
        /* Query Updaters */
        /******************/

        public DirectusQuery IsEqual(string name, string value)
        {
            UpdateFilter(name, value, "eq");

            return this;
        }

        public DirectusQuery IsEqual(string name, long value)
        {
            UpdateFilter(name, value, "eq");

            return this;
        }

        public DirectusQuery IsNotEqual(string name, string value)
        {
            UpdateFilter(name, value, "neq");

            return this;
        }

        public DirectusQuery IsNotEqual(string name, long value)
        {
            UpdateFilter(name, value, "neq");

            return this;
        }


        public DirectusQuery LessThan(string name, long value)
        {
            UpdateFilter(name, value, "lt");

            return this;
        }

        public DirectusQuery LessThan(string name, string value)
        {
            UpdateFilter(name, value, "lt");

            return this;
        }

        public DirectusQuery LessThanOrEqual(string name, string value)
        {
            UpdateFilter(name, value, "lte");

            return this;
        }

        public DirectusQuery LessThanOrEquall(string name, long value)
        {
            UpdateFilter(name, value, "lte");

            return this;
        }

        public DirectusQuery GreaterThan(string name, long value)
        {
            UpdateFilter(name, value, "gt");

            return this;
        }

        public DirectusQuery GreaterThan(string name, string value)
        {
            UpdateFilter(name, value, "gt");

            return this;
        }

        public DirectusQuery GreaterThanOrEqual(string name, string value)
        {
            UpdateFilter(name, value, "gte");

            return this;
        }

        public DirectusQuery GreaterThanOrEquall(string name, long value)
        {
            UpdateFilter(name, value, "gte");

            return this;
        }

        public DirectusQuery In<T>(string name, IEnumerable<T> values) where T : class
        {
            var value = string.Join(",", values);
            UpdateFilter(name, value, "in");

            return this;
        }

        public DirectusQuery NotIn<T>(string name, IEnumerable<T> values) where T : class
        {
            var value = string.Join(",", values);
            UpdateFilter(name, value, "nin");

            return this;
        }

        public DirectusQuery IsNull(string name)
        {
            UpdateFilter($"[{name}][null]");

            return this;
        }

        public DirectusQuery IsNotNull(string name)
        {
            UpdateFilter($"[{name}][nnull]");

            return this;
        }

        public DirectusQuery Contains(string name, string value, bool caseSensitive = true)
        {
            if(caseSensitive)
            {
                UpdateFilter(name, value, "contains");
            }
            else
            {
                UpdateFilter(name, value, "icontains");
            }

            return this;
        }

        public DirectusQuery DoesNotContains(string name, string value)
        {
            UpdateFilter(name, value, "ncontains");

            return this;
        }

        public DirectusQuery StartsWith(string name, string value, bool caseSensitive = true)
        {
            if (caseSensitive)
            {
                UpdateFilter(name, value, "starts_with");
            }
            else
            {
                UpdateFilter(name, value, "istarts_with");
            }

            return this;
        }

        public DirectusQuery DoesNotStartWith(string name, string value, bool caseSensitive = true)
        {
            if (caseSensitive)
            {
                UpdateFilter(name, value, "nstarts_with");
            }
            else
            {
                UpdateFilter(name, value, "instarts_with");
            }

            return this;
        }

        public DirectusQuery EndsWith(string name, string value, bool caseSensitive = true)
        {
            if (caseSensitive)
            {
                UpdateFilter(name, value, "ends_with");
            }
            else
            {
                UpdateFilter(name, value, "iends_with");
            }

            return this;
        }

        public DirectusQuery DoesNotEndWith(string name, string value, bool caseSensitive = true)
        {
            if (caseSensitive)
            {
                UpdateFilter(name, value, "nends_with");
            }
            else
            {
                UpdateFilter(name, value, "inends_with");
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
            UpdateFilter(name, value, "between");

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
            UpdateFilter(name, value, "nbetween");

            return this;
        }

        public DirectusQuery IsEmpty(string name)
        {
            UpdateFilter($"[{name}][empty]");

            return this;
        }

        public DirectusQuery IsNotEmpty(string name)
        {
            UpdateFilter($"[{name}][nempty]");

            return this;
        }



        /*******************/
        /* Private Methods */
        /*******************/
        private void UpdateFields(string fields)
        {
            _query += string.IsNullOrEmpty(_query) ? "?fields" : "&fields";
            _query += query;
        }

        private void UpdateFilter(string query)
        {
            _query += string.IsNullOrEmpty(_query) ? "?filter" : "&filter";
            _query += query;
        }

        private void UpdateFilter(string name, string value, string condition)
        {
            _query += string.IsNullOrEmpty(_query) ? "?filter" : "&filter";
            _query += $"[{name}][{condition}]={value}";
        }

        private void UpdateFilter(string name, long value, string condition)
        {
            _query += string.IsNullOrEmpty(_query) ? "?filter" : "&filter";
            _query += $"[{name}][_{condition}]={value}";
        }

    }
}
