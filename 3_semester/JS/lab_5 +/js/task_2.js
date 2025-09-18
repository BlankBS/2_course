function GetVolume(length)
{
    return(width) =>{
        return(height)=>{
            return length * width * height;
        }
    }
}

const fixlength = GetVolume(10);

console.log(fixlength(5)(7));
console.log(fixlength(8)(6));
console.log(fixlength(4)(7));