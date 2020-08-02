using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AwaitAsyncLibrary
{
    public class AwaitAsyncClassNew
    {
        public async void Show()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"This is Main  Start {Thread.CurrentThread.ManagedThreadId}");
                {
                    //this.NoReturn();
                    await ReturnTask();
                }
                Console.WriteLine($"This is Main   End {Thread.CurrentThread.ManagedThreadId}");
            }

        }

        private async void NoReturn()
        {
            Console.WriteLine($"This is NoReturn Start {Thread.CurrentThread.ManagedThreadId}");
            await Task.Run(() =>
            {
                Console.WriteLine($"This is NoReturn Task Start {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(2000);
                Console.WriteLine($"This is NoReturn Task   End {Thread.CurrentThread.ManagedThreadId}");
            });
            Console.WriteLine($"This is NoReturn   End {Thread.CurrentThread.ManagedThreadId}");
        }

        private async Task ReturnTask()
        {
            Console.WriteLine($"This is ReturnTask Start {Thread.CurrentThread.ManagedThreadId}");
            var task = Task.Run(() =>
            {
                Console.WriteLine($"This is ReturnTask Task Start {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(2000);
                Console.WriteLine($"This is ReturnTask Task   End {Thread.CurrentThread.ManagedThreadId}");
            });
            await task;
            Console.WriteLine($"This is ReturnTask   End {Thread.CurrentThread.ManagedThreadId}");
            //return task;
        }

        private async Task<string> ReturnString()
        {
            Console.WriteLine($"This is ReturnString Start {Thread.CurrentThread.ManagedThreadId}");
            var result = Task.Run(() =>
            {
                Console.WriteLine($"This is ReturnString Task Start {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(2000);
                Console.WriteLine($"This is ReturnString Task   End {Thread.CurrentThread.ManagedThreadId}");
                return "yuanzijun";
            });
            Console.WriteLine($"This is ReturnString   End {Thread.CurrentThread.ManagedThreadId}");
            return await result;
        }
        /// <summary>
        /// await/async 新语法，出现在c#5.0  
        /// 是一个语法糖，不是一个权限的异步多线程使用方式，
        /// (语法糖，就是编译器提供的新功能)
        /// 本身不会产生新的线程，但是依托于task而存在
        /// </summary>
        /// <returns></returns>
        public async Task DoSomething()
        {
            await Task.Run(() =>
            {
                Console.WriteLine("*****");
            });
        }
    }
}
