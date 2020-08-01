using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyAsync
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region btnSync_Click

        private void btnSync_Click(object sender, EventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine("==============btnSync_Click 同步方法 start {0}=============", Thread.CurrentThread.ManagedThreadId);
            //int j = 0;
            //int k = 1;
            //int m = j + k;
            Action<string> action = this.DoSomethingLong;
            for (int i = 0; i < 5; i++)
            {
                string name = string.Format("{0}_{1}", "btn_Sync_Click", i);
                //this.DoSomethingLong(name);
                action.Invoke(name);
            }
            Console.WriteLine("==============btnSync_Click 同步方法   end {0}=============", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine();
        }

        #endregion btnSync_Click

        #region btnAsync_Click

        /// <summary>
        /// 异步方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAsync_Click(object sender, EventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine("==============btnAsync_Click 异步方法 start {0}=============", Thread.CurrentThread.ManagedThreadId);

            Action<string> action = this.DoSomethingLong;
            //action.Invoke("btn_Sync_Click_1");
            //action("btn_Sync_Click_2");
            //action.BeginInvoke("btn_Async_Click_3", null, null);

            for (int i = 0; i < 5; i++)
            {
                string name = $"btnAsync_Click_{i}";
                action.BeginInvoke(name, null, null);
            }

            Console.WriteLine("==============btnAsync_Click 异步方法   end {0}=============", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine();
        }

        #endregion btnAsync_Click

        #region btnAsyncAdvanced_Click

        /// <summary>
        /// 异步进阶
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAsyncAdvanced_Click(object sender, EventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine("==============btnAsync_Click 异步方法 start {0:00}=============", Thread.CurrentThread.ManagedThreadId);

            Action<string> action = this.DoSomethingLong;

            #region 异步回调

            //for (int i = 0; i < 5; i++)
            //{
            //string name = $"btnAsync_Click_";

            //action.BeginInvoke(name, ar =>
            //{
            //    Console.WriteLine(ar.AsyncState);
            //    Console.WriteLine($"btnAsyncAdvanced_Click操作已经完成啦。。。{Thread.CurrentThread.ManagedThreadId}");
            //}, "AsyncAdvanced");
            //}

            #endregion 异步回调

            #region IsCompleted 等待

            {
                //IAsyncResult asyncResult = action.BeginInvoke("文件上传", null, null);
                //Action<IAsyncResult, Label> action1 = this.UpdateView;
                //action1.BeginInvoke(asyncResult, this.lblProcessing, null, null);
            }

            #endregion IsCompleted 等待

            #region 信号量

            {
                var asyncResult = action.BeginInvoke("调用接口", null, null);
                asyncResult.AsyncWaitHandle.WaitOne();//阻塞线程直到收到信号量
                Console.WriteLine("接口调用成功");
            }

            #endregion 信号量

            #region 获取返回值

            {
                Func<int> func = () => 1;
                IAsyncResult asyncResult = func.BeginInvoke(null, null);
                int iResult = func.EndInvoke(asyncResult);
                Console.WriteLine(iResult);
            }

            #endregion 获取返回值

            Console.WriteLine("==============btnAsync_Click 异步方法   end {0:00}=============", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine();
        }

        #endregion btnAsyncAdvanced_Click

        #region 文件上传

        private void UpdateView(IAsyncResult asyncResult, Label label)
        {
            int i = 0;
            while (!asyncResult.IsCompleted)
            {
                if (i < 10)
                {
                    this.ShowConsoleAndView($"当前文件上传进度为{++i * 10}%...", label);
                }
                else
                {
                    this.ShowConsoleAndView($"完成上传...", label);
                    break;
                }
                Thread.Sleep(400);
                //Console.WriteLine(");
            }
        }

        private void ShowConsoleAndView(string text, Label label)
        {
            Console.WriteLine(text);
            label.Text = text;
        }

        #endregion 文件上传

        #region PrivateMethod

        /// <summary>
        /// 一个耗时的计算
        /// </summary>
        /// <param name="name"></param>
        private void DoSomethingLong(string name)
        {
            Console.WriteLine("===================DoSomethingLong start {0} {1} {2}==================", name, Thread.CurrentThread.ManagedThreadId.ToString("00"), DateTime.Now.ToString("HHmmss:fff"));
            long LResult = 0;
            for (int i = 0; i < int.MaxValue; i++)
            {
                LResult += i;
            }

            Console.WriteLine("===================DoSomethingLong   end {0} {1} {2}==================", name, Thread.CurrentThread.ManagedThreadId.ToString("00"), DateTime.Now.ToString("HHmmss:fff"));
        }

        #endregion PrivateMethod

        #region 0801

        private void btnMultiple_Click(object sender, EventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine("******btnAsync_Click异步方法 start 线程:{0:00}******", Thread.CurrentThread.ManagedThreadId);
            {
                // .NetFramework 1.0 1.1
                //ThreadStart threadStart = () =>
                //{
                //    Console.WriteLine($"This is Thread Start {Thread.CurrentThread.ManagedThreadId}");
                //    Thread.Sleep(2000);
                //    Console.WriteLine($"This is Thread   End {Thread.CurrentThread.ManagedThreadId}");
                //};
                //Thread thread = new Thread(threadStart);
                //thread.Start();
            }

            {
                // .NetFramework 2.0(新的CLR) ThreadPool
                // 1.线程复用 2.限制最大线程数量
                //WaitCallback callback = o =>
                //{
                //    Console.WriteLine($"This is ThreadPool Start {Thread.CurrentThread.ManagedThreadId}");
                //    Thread.Sleep(2000);
                //    Console.WriteLine($"This is ThreadPool   End {Thread.CurrentThread.ManagedThreadId}");
                //};
                //ThreadPool.QueueUserWorkItem(callback);
            }

            {
                // .NetFramework 3.0 Task被称之为多线程的最佳实践！
                //Action action = () =>
                //{
                //    Console.WriteLine($"This is Task Start {Thread.CurrentThread.ManagedThreadId}");
                //    Thread.Sleep(2000);
                //    Console.WriteLine($"This is Task   End {Thread.CurrentThread.ManagedThreadId}");
                //};
                //Task task = new Task(action);
                //task.Start();
            }

            {
                // Parallel可以启动多线程，主线程也参与计算
                // ParallelOptions 控制最大并发数量
                //Parallel.Invoke(() =>
                //{
                //    Console.WriteLine($"This is Parallel Start1 {Thread.CurrentThread.ManagedThreadId}");
                //    Thread.Sleep(2000);
                //    Console.WriteLine($"This is Parallel   End1 {Thread.CurrentThread.ManagedThreadId}");
                //},
                //() =>
                //{
                //    Console.WriteLine($"This is Parallel Start2 {Thread.CurrentThread.ManagedThreadId}");
                //    Thread.Sleep(2000);
                //    Console.WriteLine($"This is Parallel   End2 {Thread.CurrentThread.ManagedThreadId}");
                //},
                //() =>
                //{
                //    Console.WriteLine($"This is Parallel Start3 {Thread.CurrentThread.ManagedThreadId}");
                //    Thread.Sleep(2000);
                //    Console.WriteLine($"This is Parallel   End3 {Thread.CurrentThread.ManagedThreadId}");
                //},
                //() =>
                //{
                //    Console.WriteLine($"This is Parallel Start4 {Thread.CurrentThread.ManagedThreadId}");
                //    Thread.Sleep(2000);
                //    Console.WriteLine($"This is Parallel   End4 {Thread.CurrentThread.ManagedThreadId}");
                //});
            }

            {
                // await async
                //await Task Run(() => { })
            }

            {
                //TaskFactory taskFactory = new TaskFactory();
                //taskFactory.ContinueWhenAll();
                //taskFactory.ContinueWhenAny();
                //Task.WaitAll();
                //Task.WaitAny();
            }
            Console.WriteLine("******btnAsync_Click异步方法   end 线程:{0:00}******", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine();
        }

        #endregion 0801
    }
}