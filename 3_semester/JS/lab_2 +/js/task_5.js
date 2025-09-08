function copyStr(n, s)
{
    let result = "";
    for (let i = 0; i < n; i++)
    {
        result += s;
    }
    return result;
}

console.log("copyStr(3, 'BlankBS') = " + copyStr(3, "BlankBS"));