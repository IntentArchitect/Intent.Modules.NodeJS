import { Entity, PrimaryGeneratedColumn, Column } from 'typeorm';

@Entity('texture')
export class Texture {

  @PrimaryGeneratedColumn('uuid')
  id?: string;
  @Column()
  textureAttribute: string;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}