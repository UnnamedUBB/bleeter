import  {Avatar, AvatarImage, AvatarFallback} from "@/components/ui/avatarpost";
import {
    Card,
    CardDescription,
    CardHeader,
    CardTitle,
} from "@/components/ui/card"
import {Link} from "react-router-dom";



function CommentContent() {

  return (
    <>
  <div className="grid mt-5 w-2/3 mx-auto">
    <Card className="p-5 shadow-lg rounded-lg border border-green-950">
      <CardHeader className="flex items-center">
        <Avatar className="mr-4">
          <AvatarImage src="https://github.com/shadcn.png" />
          <AvatarFallback>CN</AvatarFallback>
        </Avatar>
        <div>
          <CardTitle className="text-lg font-bold">Jacek Jaworek</CardTitle>
          <CardDescription className="text-sm text-gray-500">
            19:11 - 13.05.2024
          </CardDescription>
        </div>
      </CardHeader>
      <div className="mt-4">
        <p className="leading-relaxe">
          Lorem ipsum dolor sit amet consectetur adipisicing elit. Vitae
          accusantium expedita asperiores porro natus possimus labore beatae
          adipisci architecto ut eveniet dignissimos optio pariatur dolore,
          quia similique excepturi illum recusandae.
        </p>
        <img
          className="mt-4 w-full rounded-md object-cover"
          src="postimg.jpg"
          alt=""
        />
      </div>
      <div className="mt-4 flex items-center space-x-6">
        <div className="flex items-center space-x-2">
          <svg
            xmlns="http://www.w3.org/2000/svg"
            width="16"
            height="16"
            fill="currentColor"
            className="bi bi-hand-thumbs-up-fill fill-green-600 h-6 w-6"
            viewBox="0 0 16 16"
          >
            <path d="M6.956 1.745C7.021.81 7.908.087 8.864.325l.261.066c.463.116.874.456 1.012.965.22.816.533 2.511.062 4.51a10 10 0 0 1 .443-.051c.713-.065 1.669-.072 2.516.21.518.173.994.681 1.2 1.273.184.532.16 1.162-.234 1.733q.086.18.138.363c.077.27.113.567.113.856s-.036.586-.113.856c-.039.135-.09.273-.16.404.169.387.107.819-.003 1.148a3.2 3.2 0 0 1-.488.901c.054.152.076.312.076.465 0 .305-.089.625-.253.912C13.1 15.522 12.437 16 11.5 16H8c-.605 0-1.07-.081-1.466-.218a4.8 4.8 0 0 1-.97-.484l-.048-.03c-.504-.307-.999-.609-2.068-.722C2.682 14.464 2 13.846 2 13V9c0-.85.685-1.432 1.357-1.615.849-.232 1.574-.787 2.132-1.41.56-.627.914-1.28 1.039-1.639.199-.575.356-1.539.428-2.59z" />
          </svg>
          <p className="font-bold text-lg">1</p>
        </div>
        <div className="flex items-center space-x-2">
          <svg
            xmlns="http://www.w3.org/2000/svg"
            width="16"
            height="16"
            fill="currentColor"
            className="bi bi-hand-thumbs-down-fill fill-red-600 h-6 w-6"
            viewBox="0 0 16 16"
          >
            <path d="M6.956 14.534c.065.936.952 1.659 1.908 1.42l.261-.065a1.38 1.38 0 0 0 1.012-.965c.22-.816.533-2.512.062-4.51q.205.03.443.051c.713.065 1.669.071 2.516-.211.518-.173.994-.68 1.2-1.272a1.9 1.9 0 0 0-.234-1.734c.058-.118.103-.242.138-.362.077-.27.113-.568.113-.856 0-.29-.036-.586-.113-.857a2 2 0 0 0-.16-.403c.169-.387.107-.82-.003-1.149a3.2 3.2 0 0 0-.488-.9c.054-.153.076-.313.076-.465a1.86 1.86 0 0 0-.253-.912C13.1.757 12.437.28 11.5.28H8c-.605 0-1.07.08-1.466.217a4.8 4.8 0 0 0-.97.485l-.048.029c-.504.308-.999.61-2.068.723C2.682 1.815 2 2.434 2 3.279v4c0 .851.685 1.433 1.357 1.616.849.232 1.574.787 2.132 1.41.56.626.914 1.28 1.039 1.638.199.575.356 1.54.428 2.591" />
          </svg>
          <p className="font-bold text-lg">1</p>
        </div>
        <div className="flex items-center space-x-2">
          <svg
            xmlns="http://www.w3.org/2000/svg"
            width="16"
            height="16"
            fill="currentColor"
            className="bi bi-heart-fill fill-pink-600 h-6 w-6"
            viewBox="0 0 16 16"
          >
            <path
              fillRule="evenodd"
              d="M8 1.314C12.438-3.248 23.534 4.735 8 15-7.534 4.736 3.562-3.248 8 1.314"
            />
          </svg>
          <p className="font-bold text-lg">1</p>
        </div>
      </div>
    </Card>
  </div>

  <div className="w-2/3 mt-5 mx-auto">
    <Card className="p-5 shadow-lg rounded-lg border border-green-950">
      <CardHeader>
        <CardTitle className="flex items-center space-x-4">
          <Avatar>
            <AvatarImage src="https://github.com/shadcn.png" />
            <AvatarFallback>CN</AvatarFallback>
          </Avatar>
          <input
            type="text"
            placeholder="Skomentuj!"
            className="input bg-slate-900 rounded-xl text-center h-10 w-full"
          />
          <button className="flex items-center justify-center w-10 h-10 bg-green-600 rounded-full text-white">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              width="16"
              height="16"
              fill="currentColor"
              className="bi bi-image"
              viewBox="0 0 16 16"
            >
              <path d="M6.002 5.5a1.5 1.5 0 1 1-3 0 1.5 1.5 0 0 1 3 0" />
              <path d="M2.002 1a2 2 0 0 0-2 2v10a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V3a2 2 0 0 0-2-2zm12 1a1 1 0 0 1 1 1v6.5l-3.777-1.947a.5.5 0 0 0-.577.093l-3.71 3.71-2.66-1.772a.5.5 0 0 0-.63.062L1.002 12V3a1 1 0 0 1 1-1z" />
            </svg>
          </button>
          <button className="ml-2 bg-green-600 rounded-xl px-4 py-2 text-white">
            Post
          </button>
        </CardTitle>
      </CardHeader>
    </Card>
  </div>

  <div className="grid w-2/3 mt-5 mx-auto">
    <Card className="p-4 shadow-lg rounded-lg border border-green-950">
      <CardHeader>
        <CardTitle className="flex space-x-4 items-center">
          <Avatar>
            <AvatarImage src="https://github.com/shadcn.png" />
            <AvatarFallback>CN</AvatarFallback>
          </Avatar>
          <div className="rounded-lg w-full">
            <p className="">Treść komentarza</p>
          </div>
        </CardTitle>
      </CardHeader>
    </Card>
  </div>
</>
  )
}

export default CommentContent
