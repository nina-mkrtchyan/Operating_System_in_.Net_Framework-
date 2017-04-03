using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using System.Text;//UTF8Encoding



namespace HardWare
{
    class Hard_Disk
    {

            private FileStream Stream{set;get;}

            private string Path{set;get;}

            private class EventArgs_read_:EventArgs
            {
                public bool Read_Bool { set; get; }
          
                public EventArgs_read_(bool read)
                {
                Read_Bool=read;
                }
                public override string ToString()
                {

                    return String.Format("{0} Read_Bool = {1} ", base.ToString(), Read_Bool);
                }
            }

            public event EventHandler<EventArgs_read_> read_;

            protected virtual void Event_Func(EventArgs_read_ e)
            {
            EventHandler<EventArgs_read_> temp = Volatile.Read(ref read_);
            if (temp != null) temp(this, e);

            }

            public void SimulateEvent(bool read)
           {

            // Создать объект для хранения информации, которую нужно передать получателям уведомления
            EventArgs_read_ e = new EventArgs_read_(read);
            // Вызвать виртуальный метод, уведомляющий объект о событии Если ни один из производных типов не переопределяет этот метод,
            // объект уведомит всех зарегистрированных получателей уведомления
            Event_Func(e);
            }

            public Hard_Disk(string path = @"D:\HARD_DISK.txt")
            {


                this.Path = path;
                    try
                    {
                        // Delete the file if it exists.
                        if (File.Exists(path))
                        {
                            // Note that no lock is put on the
                            // file and the possibility exists
                            // that another process could do
                            // something with it between
                            // the calls to Exists and Delete.
                            File.Delete(path);
                        }
                        // Create the file.
                        Stream=File.Create(path);
                    }//end try


                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    Stream.Close();
                }


            }



            public byte[] read(long addr,int size)
            {
            int exc;

            byte[] vec = new byte[size];

            using (FileStream sr = File.OpenRead(Path))
            {


                //public virtual void Lock(long position,long length)

                //position
                //Начало диапазона для блокировки. 
                //Значение этого параметра должно быть больше или равно нулю (0).

                //length
                //Диапазон, который нужно блокировать.

                sr.Lock(addr,size);

                /*
                public override long Seek(long offset, SeekOrigin origin)
    
                addr,Int64
                Указатель относительно начальной точки origin, от которой начинается поиск
                */

                sr.Seek(addr,SeekOrigin.Begin);

                // public override int Read(byte[] array,int offset,int count)
               
                exc=sr.Read(vec,0,size);

                //array
                //При возврате из этого метода содержит указанный массив байтов,
                //в котором значения в диапазоне от offset до (offset + count - 1)) заменены байтами, считанными из текущего источника.

                //offset
                //Смещение в байтах в массиве array, в который будут помещены считанные байты

                //count
                //Максимальное число байтов, предназначенных для чтения.
                try
                {
                    if (exc != size)
                    {
                        SimulateEvent(false);//evanta uxarkum vor vochbarehajoxa kardace prce,nor heto exceptiona qcum
                        throw new Exception("can't read from HD wanted count of bytes");
                    }
                }
                catch (Exception ob)
                {
                    Console.WriteLine(ob.Message);
                }

                SimulateEvent(true);//eventa uxarkum vor barehajox kardacela prcela


                //public virtual void Unlock(long position,long length)
                sr.Unlock(addr,size);

                //position
                //Начало диапазона, который должен быть разблокирован.

                //length
                //Диапазон, который должен быть разблокирован.

            }

            return vec;

        }


            //sra void utyun@ harci taka
            public void write(long addr, byte[] data)
            {
            
                using (FileStream wr = File.OpenWrite(Path))
                {
                    //public override long Seek(long offset,SeekOrigin origin)
                    wr.Seek(addr,SeekOrigin.Begin);
                    //addr,Int64
                    //Указатель относительно начальной точки origin, от которой начинается поиск

                    // SeekOrigin.Begin
                    //Задает начальную, конечную или текущую позицию как опорную точку для offset,
                    //используя значение типа SeekOrigin
                    
                    
                    //public override void Write(byte[] array,int offset,int count)
                    wr.Write(data, 0, data.Length);
                }

            }

        }

}
