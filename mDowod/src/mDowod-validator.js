/**
 * It checks the correctness of the Polish number of the identity document, 
 * electronic mDowód, passport and residence card
 * Sprawdza poprawność numerów Polskiego dowodu,
 * mDowodu, paszportu, karty pobytu
 * Complies with ICAO 9303-3. Zgodna z ICAO 9303-3.
 * @param {string} number
 * @returns {boolean}
 */
function IsDocumentNumberCorrect(number) {
    // mDowod (electronic ID), id number, passport, residence card number 
    const regexValidNumbers = /(^[A-Z]{4}[0-9]{5}$)|(^[A-Z]{3}[0-9]{6}$)|(^[A-Z]{2}[0-9]{7}$)/;
    const regexNumberParts = /(^(?<serie>[A-Z]{2,4})(?<controlDigit>[0-9]{1})(?<restOfNumber>[0-9]{4,6})$)/;
    const digitsAndLetters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    const weigts = [7, 3, 1, 7, 3, 1, 7, 3, 1];

    if (number === undefined || number === null) {
        return false; 
    }

    const numberToCheck = number.replace(/\s+/g, "").toUpperCase();

    if (numberToCheck.match(regexValidNumbers) === null) {
        return false;
    }

    function GetLetterValue(letter) {
        return digitsAndLetters.indexOf(letter);
    }

    const { serie, controlDigit, restOfNumber } = regexNumberParts.exec(numberToCheck).groups;
    const withoutDigitControl = `${serie}${restOfNumber}`;
    var checkSum = 0;
    for (var counter = 0; counter < withoutDigitControl.length; counter += 1) {
        checkSum += GetLetterValue(withoutDigitControl.charAt(counter)) * weigts[counter];
    }

    return parseInt(controlDigit, 10) === (checkSum % 10);
}