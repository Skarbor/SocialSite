function CheckIfArrayContainsUserByUserId(array, userId)
{
    for (var i = 0; i < array.length; i++) {
        if (array[i].UserId == userId)
        {
            return true;
        }
    }
    return false;
}

function GetArrayElementIndexByUserId(array, userId) {
    for (var i = 0; i < array.length; i++) {
        if (array[i].UserId == userId) {
            return i;
        }
    }
    return -1;
}