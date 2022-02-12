using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peliculas.BM.Helper
{
    public class TypeBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var nombreProiedad = bindingContext.ModelName;
            var proveedorDeValores = bindingContext.ValueProvider.GetValue(nombreProiedad);

            if (proveedorDeValores  == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }


            try
            {
                var valorDeserializado = JsonConvert.DeserializeObject<List<int>>(proveedorDeValores.FirstValue);
                bindingContext.Result = ModelBindingResult.Success(valorDeserializado);
 
            }
            catch (Exception)
            {
                bindingContext.ModelState.TryAddModelError(nombreProiedad, "Valor inválido para tipo de List<int>");
            }

            return Task.CompletedTask;

        }
    }
}
