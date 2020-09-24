

namespace SRB_Chart
{
    public class PlotGroup
    {
        StepResizeable<double> x;
        Plot[] plots;
        public IPlot[] Plots { get => plots; }
        public PlotGroup(int Dimension)
        {
            plots = new Plot[Dimension];
            for(int i=0; i<plots.Length; i++)
            {
                plots[i] = new Plot(this, 1024 * 16);
            }
            x = new StepResizeable<double>(1024 * 16);
        }
        public void append(double y, double[] v)
        {
            this.x.append(y);
            for (int i = 0; i < plots.Length; i++)
            {
                plots[i].y.append(v[i]);
            }
        }
        class Plot : IPlot
        {
            public StepResizeable<double> y;
            public override int Length => y.Length;
            PlotGroup pg;
            public Plot(PlotGroup plot_group, int piece_size)
            {
                this.y = new StepResizeable<double>(piece_size);
                this.pg = plot_group;
            }
            public override (double X ,double Y) this[int i]
            {
                get => (pg.x[i], y[i]); 
            }
        }

    }
}