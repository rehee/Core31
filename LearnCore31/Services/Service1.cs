using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnCore31.Services
{

  public interface IService
  {
    string GetThisType();
  }
  public class Service1<T> : IService
  {
    public string GetThisType()
    {
      return typeof(T).ToString();
    }
  }
}
