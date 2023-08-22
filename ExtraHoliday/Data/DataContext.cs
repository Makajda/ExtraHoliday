namespace ExtraHoliday.Data;
public class DataContext(DaysData daysData) {
    public async Task Initialize() {
        Days = await daysData.GetAll();
    }

    public List<Pitstop> Pitstops { get; } = PitstopsData.GetAll();
    public List<Day> Days { get; private set; }

    public Task Save() => daysData.Save(Days);
}
