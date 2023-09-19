import { Entity, PrimaryGeneratedColumn, Column } from 'typeorm';

@Entity('tpc_abstract_base_class')
export abstract class TpcAbstractBaseClass {
  @PrimaryGeneratedColumn('uuid')
  id: string;

  @Column({ length: 250 })
  baseAttribute: string;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}