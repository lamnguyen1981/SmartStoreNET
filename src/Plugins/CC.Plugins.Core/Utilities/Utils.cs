using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CC.Plugins.Core.Utilities
{
    public static class Utils
    {
        public static TDest Map<TSource, TDest>(TSource source)
                                    where TSource : class//We are not creating an instance of source, no need to restrict parameterless constructor
                                    where TDest : class, new()//We are creating an instance of destination, parameterless constructor is needed
        {
            if (source == null)
                return null;

            TDest destination = new TDest();

            var typeOfSource = source.GetType();
            var typeOfDestination = destination.GetType();

            foreach (var fieldOfSource in typeOfSource.GetFields())
            {
                var fieldOfDestination = typeOfDestination.GetField(fieldOfSource.Name);
                if (fieldOfDestination != null)
                {
                    try
                    { fieldOfDestination.SetValue(destination, fieldOfSource.GetValue(source)); }
                    catch (ArgumentException) { }//If datatype is mismatch, skip the mapping
                }
            }

            foreach (var propertyOfSource in typeOfSource.GetProperties())
            {
                var propertyOfDestination = typeOfDestination.GetProperty(propertyOfSource.Name);
                if (propertyOfDestination != null)
                {
                    try
                    { propertyOfDestination.SetValue(destination, propertyOfSource.GetValue(source)); }
                    catch (ArgumentException) { }//If datatype is mismatch, skip the mapping
                }
            }

            return destination;
        }
    }
}