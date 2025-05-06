namespace ExtraHoliday.Data;
public class DataContext() {
    public async Task Initialize() {
        Days = await DaysData.GetAll();
    }

    public List<Pitstop> Pitstops { get; } = PitstopsData.GetAll();
    public List<Day> Days { get; private set; }

    public Task Save() => DaysData.Save(Days);
}
