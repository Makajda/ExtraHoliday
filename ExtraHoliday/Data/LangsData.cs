// Copyright (c) Makajda. All rights reserved. See LICENSE.md file in the solution root for full license information.

namespace ExtraHoliday.Data;
public enum Langs {
    en, de, es, fr, jp, pt, ru, ua, zh
}
public enum Lankeys {
    Day, Hour, Jupiter, Mars, Minute, Month, Moon, Saturn, Second, Venus, Week, Year
}
public class Ln {
    readonly Langs lang = Langs.en;
    public Ln() {
        try {
            if(Enum.TryParse(Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName, out Langs lang))
                this.lang = lang;
        }
        catch (Exception) { }
    }
#pragma warning disable CS8524 // The switch expression does not handle some values of its input type (it is not exhaustive) involving an unnamed enum value.
    public string this[Lankeys key] => lang switch {
        Langs.en => En(key),
        Langs.de => De(key),
        Langs.es => Es(key),
        Langs.fr => Fr(key),
        Langs.jp => Jp(key),
        Langs.pt => Pt(key),
        Langs.ru => Ru(key),
        Langs.ua => Ua(key),
        Langs.zh => Zh(key),
    };

    static string En(Lankeys key) => key switch {
        Lankeys.Day => "Days",
        Lankeys.Hour => "Hours",
        Lankeys.Jupiter => "Years on Jupiter",
        Lankeys.Mars => "Years on Mars",
        Lankeys.Minute => "Minutes",
        Lankeys.Month => "Months",
        Lankeys.Moon => "Walking to the Moon",
        Lankeys.Saturn => "Years on Saturn",
        Lankeys.Second => "Seconds",
        Lankeys.Venus => "Years on Venus",
        Lankeys.Week => "Weeks",
        Lankeys.Year => "Years",
    };

    static string De(Lankeys key) => key switch {
        Lankeys.Day => "Tage",
        Lankeys.Hour => "Stunden",
        Lankeys.Jupiter => "Jahren auf Jupiter",
        Lankeys.Mars => "Jahren auf Mars",
        Lankeys.Minute => "Minuten",
        Lankeys.Month => "Monaten",
        Lankeys.Moon => "Zu Fuß zum Mond",
        Lankeys.Saturn => "Jahren auf Saturn",
        Lankeys.Second => "Sekunden",
        Lankeys.Venus => "Jahren auf Venus",
        Lankeys.Week => "Wochen",
        Lankeys.Year => "Jahre",
    };

    static string Es(Lankeys key) => key switch {
        Lankeys.Day => "Días",
        Lankeys.Hour => "Horas",
        Lankeys.Jupiter => "Años en Júpiter",
        Lankeys.Mars => "Años en Marte",
        Lankeys.Minute => "Minutos",
        Lankeys.Month => "Meses",
        Lankeys.Moon => "Caminando a la Luna",
        Lankeys.Saturn => "Años en Saturno",
        Lankeys.Second => "Segundos",
        Lankeys.Venus => "Años en Venus",
        Lankeys.Week => "Semanas",
        Lankeys.Year => "Años",
    };

    static string Fr(Lankeys key) => key switch {
        Lankeys.Day => "Jours",
        Lankeys.Hour => "Heures",
        Lankeys.Jupiter => "Années sur Jupiter",
        Lankeys.Mars => "Années sur Mars",
        Lankeys.Minute => "Minutes",
        Lankeys.Month => "Mois",
        Lankeys.Moon => "Marcher jusqu'à la Lune",
        Lankeys.Saturn => "Années sur Saturne",
        Lankeys.Second => "Secondes",
        Lankeys.Venus => "Années sur Vénus",
        Lankeys.Week => "Semaines",
        Lankeys.Year => "Années",
    };

    static string Jp(Lankeys key) => key switch {
        Lankeys.Day => "日数",
        Lankeys.Hour => "営業時間",
        Lankeys.Jupiter => "木星の年",
        Lankeys.Mars => "火星の年",
        Lankeys.Minute => "議事録",
        Lankeys.Month => "月の",
        Lankeys.Moon => "月まで歩いて",
        Lankeys.Saturn => "土星の年",
        Lankeys.Second => "秒",
        Lankeys.Venus => "金星の年",
        Lankeys.Week => "週間",
        Lankeys.Year => "年数",
    };

    static string Pt(Lankeys key) => key switch {
        Lankeys.Day => "Dias",
        Lankeys.Hour => "Horas",
        Lankeys.Jupiter => "Anos em Júpiter",
        Lankeys.Mars => "Anos em Marte",
        Lankeys.Minute => "Minutos",
        Lankeys.Month => "Meses",
        Lankeys.Moon => "A pé até a lua",
        Lankeys.Saturn => "Anos em Saturno",
        Lankeys.Second => "Segundos",
        Lankeys.Venus => "Anos em Vênus",
        Lankeys.Week => "Semanas",
        Lankeys.Year => "Anos",
    };

    static string Ru(Lankeys key) => key switch {
        Lankeys.Day => "Дней",
        Lankeys.Hour => "Часов",
        Lankeys.Jupiter => "Лет на Юпитере",
        Lankeys.Mars => "Лет на Марсе",
        Lankeys.Minute => "Минут",
        Lankeys.Month => "Месяцев",
        Lankeys.Moon => "Пешком до Луны",
        Lankeys.Saturn => "Лет на Сатурне",
        Lankeys.Second => "Секунд",
        Lankeys.Venus => "Лет на Венере",
        Lankeys.Week => "Недель",
        Lankeys.Year => "Лет",
    };

    static string Ua(Lankeys key) => key switch {
        Lankeys.Day => "Днiв",
        Lankeys.Hour => "Годин",
        Lankeys.Jupiter => "Рокiв на Юпiтерi",
        Lankeys.Mars => "Рокiв на Марсi",
        Lankeys.Minute => "Хвилин",
        Lankeys.Month => "Мiсяцiв",
        Lankeys.Moon => "Пішки до Місяця",
        Lankeys.Saturn => "Рокiв на Сатурнi",
        Lankeys.Second => "Секунд",
        Lankeys.Venus => "Рокiв на Венерi",
        Lankeys.Week => "Тижнів",
        Lankeys.Year => "Рокiв",
    };

    static string Zh(Lankeys key) => key switch {
        Lankeys.Day => "天数",
        Lankeys.Hour => "营业时间",
        Lankeys.Jupiter => "木星岁月",
        Lankeys.Mars => "火星岁月",
        Lankeys.Minute => "分钟",
        Lankeys.Month => "数月",
        Lankeys.Moon => "徒步前往月球",
        Lankeys.Saturn => "土星岁月",
        Lankeys.Second => "秒",
        Lankeys.Venus => "金星岁月",
        Lankeys.Week => "周数",
        Lankeys.Year => "年数",
    };
#pragma warning restore CS8524 // The switch expression does not handle some values of its input type (it is not exhaustive) involving an unnamed enum value.
}
