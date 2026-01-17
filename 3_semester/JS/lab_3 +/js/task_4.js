function ConevertStrings(str)
{
    let total1 = "";
    let total2 = "";

    for (let i = 0;i < str.length; i++)
    {
        let ch = str.charCodeAt(i).toString();
        total1 += ch;

        if(ch.includes("7"))
        {
            ch = ch.replace("7", "1");
        }
        total2 += ch;
    }
    console.log(`total1: ${total1}`);
    console.log(`total2: ${total2}`);
    console.log(`total1 - total2: ${total1 - total2}`);
}

str = "ABC";
ConevertStrings(str);