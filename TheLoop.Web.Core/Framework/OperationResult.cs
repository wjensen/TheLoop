using System;
using System.Collections;

namespace TheLoop.Web.Core.Framework
{
	public class OperationResult
	{
		private readonly Hashtable results = new Hashtable(StringComparer.OrdinalIgnoreCase);
		private TimeSpan elapsedTime;
		private DateTime end;
		private Exception exception;
		private Guid guid;
		private DateTime start;

		public OperationResult()
		{
			guid = Guid.NewGuid();
		}

		public object Result
		{
			get
			{
				if (results.ContainsKey(guid.ToString()))
				{
					return GetResult(guid.ToString());
				}
				return null;
			}
			set { UpdateResult(guid.ToString(), value); }
		}

		public bool IsValid
		{
			get { return exception == null; }
		}

		public Exception GetException
		{
			get { return exception; }
		}

		public TimeSpan ElapsedTime
		{
			get { return elapsedTime; }
		}

		public virtual string GetDebugMessage()
		{
			return null;
		}

		public void UpdateResult(string key, object value)
		{
			if (results.ContainsKey(key) == false)
			{
				results.Add(key, value);
			}
			else
			{
				results[key] = value;
			}
		}

		public void AddResult(string key, object value)
		{
			if (results.ContainsKey(key) == false)
			{
				results.Add(key, value);
			}
		}

		public object GetResult(string key)
		{
			if (results.ContainsKey(key))
			{
				return results[key];
			}
			return null;
		}

		public void AddException(Exception ex)
		{
			exception = ex;
		}

		public void Start()
		{
			start = DateTime.Now;
		}

		public void End()
		{
			end = DateTime.Now;
			elapsedTime = end - start;
		}
	}
}