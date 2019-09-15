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
      internal string fileName;

      public bool IsReady()
      {
        return this._initTask.Status == TaskStatus.RanToCompletion;
      }

      public void WaitForLoading()
      {
        if(this._initTask.Status == TaskStatus.Running)
        {
          Console.WriteLine($"Waiting for {this.fileName} to load");
          this._initTask.Wait();
          return;
        }
        return;
      }

      public virtual Task InitAsync(string file)
      {
        this.fileName = Path.GetFileName(file);
        return Task.CompletedTask;
      }

  }
}