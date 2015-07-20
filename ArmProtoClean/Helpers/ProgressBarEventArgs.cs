using System;

namespace ArmProtoClean.Helpers
{

    //class ProgressBarEventArgs
    //{
    //}
    public class ProgressBarEventArgs : EventArgs
    {

        public ProgressBarEventArgs(int progress, int maximum)
        {
            Progress = progress;
            Maximum = maximum;
        }

        public ProgressBarEventArgs(int progress) { Progress = progress; }

        public int Progress { get; set; }

        public int Maximum { get; set; }

    }

}