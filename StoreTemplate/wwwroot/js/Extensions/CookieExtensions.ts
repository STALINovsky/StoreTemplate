﻿
export function getCookie(name: string): string {
    let cookieName = name + "=";

    let cookieRecords = document.cookie.split(";");
    for (let i = 0; i < cookieRecords.length; i++) {
        let currentRecord = cookieRecords[i];

        while (currentRecord.charAt(0) === " ") {
            currentRecord = currentRecord.substring(1, currentRecord.length);
        }

        if (currentRecord.indexOf(cookieName) === 0)
            return currentRecord.substring(cookieName.length, currentRecord.length);
    }
    return null;
}

