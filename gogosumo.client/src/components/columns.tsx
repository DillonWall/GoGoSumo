import { ColumnDef } from "@tanstack/react-table"

// This type is used to define the shape of our data.
// You can use a Zod schema here if you want.
export type UserEntity = {
    clerkId: string
    userPhone: string
    userFluentLanguages: string[]
    roleName: string
}

export const columns: ColumnDef<UserEntity>[] = [
    {
        accessorKey: "clerkId",
        header: "ID",
    },
    {
        accessorKey: "userPhone",
        header: "Phone",
    },
    {
        accessorKey: "userFluentLanguages",
        header: "Fluent Languages",
    },
    {
        accessorKey: "roleName",
        header: "Role Name",
    },
]
