function getString(s1 = "первый", s2, s3 = prompt("Введите 3-й параметр")){
    return s1 + ' ' + s2 + ' ' + s3;
}

alert(getString());