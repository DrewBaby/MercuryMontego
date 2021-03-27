var imgScanner = document.getElementsByName(div);
var imgScanner = document.getElementById("test");

const arrBusinessName= ['sex','is','fun'];

for (let i=0;i < arrBusinessName.length; i++ ){
    console.log ("This is a test of the broadcasting")
}



const apiKey = '527bd1eef1c04bc68d1b5eb33e9b7cb2';
const url = 'https://api.rebrandly.com/v1/links';
// Some page elements
const inputField = document.querySelector('#input');
const shortenButton = document.querySelector('#shorten');
const responseField = document.querySelector('#responseField');
// AJAX functions
const shortenUrl = () => {
  const urlToShorten=inputField.value
  const data=JSON.stringify({destination: urlToShorten});
  fetch(url,{
    method: 'POST',
    headers: {
                'Content-type': 'application/json',
                'apikey': apiKey
             } ,
    body: data
  })
  .then((response)=>{
      if (response.ok){
        return response.json();
      } throw new Error ('Request failed!')
  }, networkError => {
    //console.log(networkError.message);
    //return networkError.message;
    renderRawResponse(networkError);
    }
  )
  .then((jsonResponse)=>{
      //renderRawResponse(jsonResponse);
      renderResponse(jsonResponse);
  });
}
// Clear page and call AJAX functions
const displayShortUrl = (event) => {
  event.preventDefault();
  while(responseField.firstChild){
    responseField.removeChild(responseField.firstChild)
  }
  shortenUrl();
}
shortenButton.addEventListener('click', displayShortUrl);


