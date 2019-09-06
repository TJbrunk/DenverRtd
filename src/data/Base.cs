using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;

namespace RtdData
{
  public abstract class Base
  {
      internal Task _initTask;

      public bool IsReady()
      {
        return this._initTask.Status == TaskStatus.RanToCompletion;
      }

      public void WaitForLoading()
      {
        if(this._initTask.Status == TaskStatus.Running)
        {
          Console.WriteLine("Waiting for file to load");
          this._initTask.Wait();
          return;
        }
        return;
      }

      public abstract Task InitAsync(string file);

  }
}