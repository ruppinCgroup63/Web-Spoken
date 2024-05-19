using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

public class TextCorrector
{
    //שימוש במודל של האגין פייס שנקרא טי חמש בייס גראמר קורקשיין
    private readonly string apiUrl = "https://api-inference.huggingface.co/models/vennify/t5-base-grammar-correction";
   //התוקן של האייפי שלנו המפתח שלנו לשימוש בשירות שלהם
    private readonly string apiToken = "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";
   //מספר הפעמים המקסימלי שבו המתודה תנסה לבצע את הבקשה במקרה שלא הצליחה
    private readonly int maxRetries = 5;
   //משך הזמן שתחכה המתודה בין ניסיונות חוזרים במילישניות
    private readonly int delayMilliseconds = 2000;

    //מתודה אסינכרונית 
    public async Task<string> CorrectGrammarAsync(string text)
    {
        //משתמשים ביוזינג על מנת שלא נצטרך לשחרר את המשאבים אחכ כלומר הם משתחררים אוטומטית
        //מייצרים אובייקט קליינט שמשמש לביצוע בקשות http
        using (HttpClient client = new HttpClient())
        {
            //שליחת בקשה חדשה לאימות מול השרת עם התוקן שלנו
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);

            //יוצרים את גוף הבקשה בפורמט של סטרינג שמכיל את הטקסט שנרצה לתקן,ממירים ל"utf8"
            //"application/json"ומגדירים את סוג התוכן ל
            var jsonContent = new StringContent("{\"inputs\":\"" + text + "\"}", Encoding.UTF8, "application/json");

            //אתחול של משתנה מספר הניסיות החוזרים
            int retryCount = 0;
            //הלולאה מתחילה לעבוד וממשיכה עד שמקבלת תשובה מוצלחת או עד שמס' הניסיונות מגיע למקסימום
            while (true)
            {
                try
                {
                    //מתבצעת בקשת פוסט אסינכרונית לאייפיאי באמצעות היואראל והתוכן שלנו
                    HttpResponseMessage response = await client.PostAsync(apiUrl, jsonContent);
                    //מוודאת שהבקשה הושלמה בהצלחה אם לא תיזרק חריגה 
                    response.EnsureSuccessStatusCode();
                    //קריאת התגובה שהתקבלה מהשרת כמחרוזת 
                    string result = await response.Content.ReadAsStringAsync();

                    // הופכת את התגובה שהתקבלה מהשרת למערך של גייסון
                    JArray jsonResult = JArray.Parse(result);
                    //ניגשים לפריט הראשון במערך וניגשים לערך בתוך האובייקט גייסון וממירים אותו לסטרינג
                    string correctedText = jsonResult[0]["generated_text"].ToString();
                    //החזרת הטקסט המתוקן שהתקבל מההאגין פייס
                    return correctedText;
                }
                //טיפול בחריגה אם מספר הנסיונות קטן מהמקסימום נגדיל את הניסיונות
                catch (HttpRequestException ex) when (retryCount < maxRetries)
                {
                    retryCount++;
                    Console.WriteLine($"Request failed with {ex.Message}. Retrying ({retryCount}/{maxRetries})...");
                  //ממתינים למשך הזמן המוגדר לפני ניסיון חוזר
                    await Task.Delay(delayMilliseconds);
                }
            }
        }
    }
}



