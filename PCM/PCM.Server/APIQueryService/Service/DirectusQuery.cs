using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Collections;
using System.Reflection.Metadata.Ecma335;

namespace PCM.Server.APIQueryService.Service
{
    public class DirectusQuery
    {
        private string? _baseUrl { get; set; }

        private List<string> _fields { get; set; } = new List<string>();

        private List<string> _filters { get; set; } = new List<string>();

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

                return _baseUrl + GetQueryString();
            }
        }

        public string GetQueryString()
        {
            var query = "";

            if (_fields.Any() || _filters.Any())
                query = "?";

            if (_fields.Any())
                query += "fields=" + string.Join(",", _fields);
            
            if(_filters.Any())
            {
                if(query.Length > 1) query += "&";
                query += string.Join("&", _filters);
            }                

            return query;
        }

        /*****************/
        /* Field Methods */
        /*****************/

        public DirectusQuery AddField(string fieldName)
        {
            _fields.Add(fieldName);

            return this;
        }

        public DirectusQuery AddFields(IEnumerable<string> fields)
        {
            _fields.AddRange(fields);

            return this;
        }

        public DirectusQuery SetFields(IEnumerable<string> fields)
        {
            _fields = fields.ToList();

            return this;
        }

        public DirectusQuery IncludeRelatedFields(int depth = 1)
        {
            var field = Enumerable.Repeat("*", depth + 1);
            _fields.Add(string.Join(".", field));

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
        private void UpdateFilter(string query)
        {
            if (!query.StartsWith("filter"))
                query = $"filter{query}";

            _filters.Add(query);
        }

        private void UpdateFilter(string name, string value, string condition)
        {
            if(name.Contains("."))
            {
                var names = name.Split('.');
                name = "";
                foreach(var n in names)
                {
                    name += $"[{n}]";
                }
            }
            else
            {
                name = $"[{name}]";
            }

            _filters.Add($"filter{name}[_{condition}]={value}");
        }

        private void UpdateFilter(string name, long value, string condition)
        {
            UpdateFilter(name, value.ToString(), condition);
        }

    }
}
