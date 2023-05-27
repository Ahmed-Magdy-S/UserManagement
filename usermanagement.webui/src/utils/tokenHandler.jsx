const tokenHandler = {

  getToken: (name) => {
    const cookieValue = document.cookie
      .split("; ")
      .find((row) => row.startsWith(`${name}=`))
      ?.split("=")[1];
    return cookieValue;
  },

  setToken: (name, value, expirationDate) => {
    document.cookie = `${name}=${value}; expires=${expirationDate}`;
  },

  deleteToken: (name) => {
    document.cookie = `${name}=; expires=Thu, 01 Jan 1970 00: 00: 00 GMT`;
  }

}

export { tokenHandler }



