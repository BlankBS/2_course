function ReverseString(str)
{
    let result = "";
    for(let i = str.length; i >= 0; i--) {
        if(str[i] >= "A" && str[i] <= "z") result += str[i];        
    }
    return result;
}

console.log("ReverseString('JavaScr53Э? ipt') = " + ReverseString("JavaScr53Э? ipt"));