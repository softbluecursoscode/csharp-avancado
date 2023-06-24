using System;
using System.Reflection;

namespace AutoRun
{
    public class Result
    {
        public Type ClassType { get; private set; }
        public MethodInfo Method { get; private set; }
        public TimeSpan Time { get; private set; }

        public Result(Type classType, MethodInfo method, TimeSpan time)
        {
            ClassType = classType;
            Method = method;
            Time = time;
        }

        public override string ToString()
        {
            return string.Format("Classe: {0}, Método: {1}, Tempo: {2:00}:{3:00}:{4:00}.{5:000}", ClassType.Name, Method.Name, Time.Hours, Time.Minutes, Time.Seconds, Time.Milliseconds);
        }
    }
}
