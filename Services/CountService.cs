namespace aspnetcore3_demo.Services {
    public class CountService {
        public int _count;
        public int GetLstestCount () {
            return _count++;
        }
    }
}
