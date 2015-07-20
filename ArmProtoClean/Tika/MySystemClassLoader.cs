using ikvm.runtime;
using java.lang;

namespace ArmProtoClean.Tika
{

    //class MySystemClassLoader
    //{
    //}

    public class MySystemClassLoader : ClassLoader
    {

        public MySystemClassLoader(ClassLoader parent)
            : base(new AppDomainAssemblyClassLoader(typeof (MySystemClassLoader).Assembly))
        {
            //empty (don Comment)
        }

    }

}