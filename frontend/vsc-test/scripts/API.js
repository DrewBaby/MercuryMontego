const url='https://api.github.com/repos/walmartlabs/thorax/issues';

// const getListIssues =  ()=>{
    
//     fetch (url, {
//         method:'GET',
       
//     })  
//         .then ((res)=>{
//         if (res.ok) {
//             return res.json()
//             // .then((rres) => {
//             // console.log(rres);
//             // });
//         } 
//     }, networkError => console.log (networkError.message));
// };

// console.log ('outerloop')
// const t = getListIssues();
// console.log (t);
// for (key in t) {
//     console.log(key);
//     console.log ('test');
// }


const checkAuth = async () => {
    const data = await fetch(url,{method:'GET'}) 
                .then(response => response.json())
                    //   .then(json => {return json})
    return data;
    }

   console.log (checkAuth())
   
   checkAuth().then (dd =>{
    for (key in dd ) {
            console.log(key);
            console.log (dd[key]);
            for (key2 in dd[key]){
                console.log(key2);
            }
        }
    // dd.forEach(element => {
    //     console.log (element);
        
    // });
    //console.log (dd);

   }
   );

    // t.forEach (async key => {
    //     console.log (key);
    // });
    
//     for (key in t ) {
//     console.log(key);
//     console.log ('test');
// }


// const rd = getListIssues();
// console.log (rd);
// console.log ('Test');

// const url='https://api.github.com/repos/walmartlabs/thorax/issues';
// fetch(url, {
//   method: 'GET'
// }).then((response) => {
//   response.json().then((jsonResponse) => {
//     console.log(jsonResponse)
//   })
//  }).catch((err) => {
//   console.log(`Error: ${err}` )
// });