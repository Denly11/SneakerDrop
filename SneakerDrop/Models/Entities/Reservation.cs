namespace SneakerDrop.Models.Entities;

public class Reservation
{
    /*Údaj	Omezení
název	povinný, 3–50 znaků
značka	povinná
popisek	nepovinný, nejvýše 200 znaků
cena	povinná, 1–100 000 Kč
počet kusů	povinný, 0–1000
datum startu	povinné; u nového dropu musí být v budoucnosti*/

    public string CustomerName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int Size { get; set; } = 0;
    public DateTime CreationDate { get; set; }
}
