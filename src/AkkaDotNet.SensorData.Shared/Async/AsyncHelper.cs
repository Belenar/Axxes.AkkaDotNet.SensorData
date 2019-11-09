using System;
using System.Threading;
using System.Threading.Tasks;

namespace AkkaDotNet.SensorData.Shared.Async
{
    public static class AsyncHelper
    {
        private static readonly TaskFactory MyTaskFactory = new
            TaskFactory(CancellationToken.None,
                TaskCreationOptions.None,
                TaskContinuationOptions.None,
                TaskScheduler.Default);

        public static TResult RunSync<TResult>(this Func<Task<TResult>> func)
        {
            return MyTaskFactory
                .StartNew<Task<TResult>>(func)
                .Unwrap<TResult>()
                .GetAwaiter()
                .GetResult();
        }

        public static void RunSync(this Func<Task> func)
        {
            MyTaskFactory
                .StartNew<Task>(func)
                .Unwrap()
                .GetAwaiter()
                .GetResult();
        }
    }
}
